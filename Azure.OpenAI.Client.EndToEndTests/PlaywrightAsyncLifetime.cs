// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.EndToEndTests;

public partial class PlaywrightAsyncLifetime : IAsyncLifetime
{
    private IPlaywright? _playwright;

    private Lazy<Task<IBrowser>> _chromiumBrowser = null!;
    private Lazy<Task<IBrowser>> _firefoxBrowser = null!;
    private Lazy<Task<IBrowser>> _webkitBrowser = null!;

    public async Task InitializeAsync()
    {
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = Debugger.IsAttached is false,
        };

        EnsurePlaywrightIsInstalled();

        // Has no effect on Linux.
        // await TrustDeveloperCertsAsync();

        _playwright = await Playwright.CreateAsync();

        _chromiumBrowser = new Lazy<Task<IBrowser>>(
          _playwright.Chromium.LaunchAsync(launchOptions));
        _firefoxBrowser = new Lazy<Task<IBrowser>>(
          _playwright.Firefox.LaunchAsync(launchOptions));
        _webkitBrowser = new Lazy<Task<IBrowser>>(
          _playwright.Webkit.LaunchAsync(launchOptions));
    }

    public async Task DisposeAsync()
    {
        if (_playwright is not null)
        {
            static async ValueTask TryDisposeBrowserAsync(Lazy<Task<IBrowser>> browserTask)
            {
                if (browserTask is { IsValueCreated: true })
                {
                    var browser = await browserTask.Value;
                    await browser.DisposeAsync();
                }
            }

            await TryDisposeBrowserAsync(_chromiumBrowser);
            await TryDisposeBrowserAsync(_firefoxBrowser);
            await TryDisposeBrowserAsync(_webkitBrowser);

            _playwright.Dispose();
            _playwright = null;
        }
    }

    private static void EnsurePlaywrightIsInstalled()
    {
        var exitCode = PlaywrightProgram.Main(new[] { "install-deps" });
        if (exitCode is not 0)
        {
            throw new Exception($"""
                Playwright exited: {exitCode} when calling 'install-deps'
                """);
        }

        exitCode = PlaywrightProgram.Main(new[] { "install" });
        if (exitCode is not 0)
        {
            throw new Exception($"""
                Playwright exited: {exitCode} when calling 'install'
                """);
        }
    }

    private static async Task TrustDeveloperCertsAsync()
    {
        var result = await Cli.Wrap("dotnet")
            .WithArguments(builder =>
                builder.Add("dev-certs")
                    .Add("https")
                    .Add("--trust"))
            .ExecuteAsync();

        if (result.ExitCode is not 0)
        {
            throw new Exception($"""
                dotnet exited: {result.ExitCode} when calling 'dev-certs https --trust'
                """);
        }
    }

    internal async Task TestPageAsync(
        string url,
        Func<IPage, IBrowserContext, Task> testHandler,
        BrowserName browserName,
        [CallerMemberName] string methodName = "")
    {
        var browser = await SelectBrowserAsync(browserName);

        await using var context = await browser.NewContextAsync(
            new BrowserNewContextOptions
            {
                IgnoreHTTPSErrors = true
            }).ConfigureAwait(false);

        await context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true,
            Title = methodName
        });

        var page = await context.NewPageAsync();
        Assert.NotNull(page);

        try
        {
            var response = await page.GotoAsync(
              url,
              new PageGotoOptions
              {
                  WaitUntil = WaitUntilState.NetworkIdle
              });
            Assert.NotNull(response);

            await response.FinishedAsync();
            Assert.True(response.Ok);

            await testHandler(page, context);
        }
        finally
        {
            await page.CloseAsync();
        }
    }

    private Task<IBrowser> SelectBrowserAsync(BrowserName browser) =>
        browser switch
        {
            BrowserName.Chromium => _chromiumBrowser.Value,
            BrowserName.Firefox => _firefoxBrowser.Value,
            BrowserName.Webkit => _webkitBrowser.Value,

            _ => throw new NotImplementedException(),
        };
}
