// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.EndToEndTests;

[Collection(PlaywrightCollectionDefinition.EndToEndTests)]
public sealed partial class TestPageLoad
{
    private const string TestHost = "http://localhost:4200";

    private readonly PlaywrightAsyncLifetime _asyncLifetime;

    public TestPageLoad(PlaywrightAsyncLifetime asyncLifetime) => _asyncLifetime = asyncLifetime;

    private async Task StartHostAndTestPageAsync(
        string url,
        Func<IPage, IBrowserContext, Task> testHandler,
        BrowserName browserName)
    {
        using var factory = new EndToEndHostFactory();
        using var program = factory.WithWebHostBuilder(
            builder => builder.UseUrls(TestHost));

        // This is needed to have the host serve HTTP requests.
        using var _ = program.CreateDefaultClient();

        await _asyncLifetime.TestPageAsync(url, testHandler, browserName);
    }

    [Theory, InlineData(BrowserName.Chromium), InlineData(BrowserName.Firefox)]
    public Task TogglingAppThemeCorrectlyRendersChangesTest(BrowserName browserName) =>
        StartHostAndTestPageAsync(
            TestHost,
            async (page, context) =>
            {
                const string PrefersDarkThemeKey = "prefers-dark-scheme";

                await ExpectedThemeStateAsync(context, PrefersDarkThemeKey, false);

                await page.GetByRole(AriaRole.Button,
                    new() { Name = "Switch to Dark Theme" }).ClickAsync();

                await ExpectedThemeStateAsync(context, PrefersDarkThemeKey, true);
                await page.GetByRole(AriaRole.Button,
                    new() { Name = "Switch to Light Theme" }).ClickAsync();

                await ExpectedThemeStateAsync(context, PrefersDarkThemeKey, false);

                static async Task ExpectedThemeStateAsync(IBrowserContext cxt, string key, bool expected)
                {
                    var json = await cxt.StorageStateAsync();
                    var settings = new JsonSerializerOptions(JsonSerializerDefaults.Web)
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var state = JsonSerializer.Deserialize<StorageState>(json, settings);

                    Assert.NotNull(state);
                    if (expected)
                    {
                        var value =
                            state.Origins[0].LocalStorage
                                .FirstOrDefault(s => s.Name == key)
                                ?.Value
                                ?? "false";

                        Assert.True(bool.Parse(value ));
                    }
                }
            },
            browserName);

    [Theory, InlineData(BrowserName.Chromium), InlineData(BrowserName.Firefox)]
    public Task SelectingLanguageDialogDisplaysModalTest(BrowserName browserName) =>
        StartHostAndTestPageAsync(
            TestHost,
            async (page, context) =>
            {
                await page.ClickAsync(HTMLSelectors.SelectLanguageButton);
 
                var locator = page.Locator("div.mud-overlay.mud-overlay-dialog");
                await Assertions.Expect(locator).ToBeVisibleAsync();
            },
            browserName);

    [Theory, InlineData(BrowserName.Chromium), InlineData(BrowserName.Firefox)]
    public Task TogglingSidebarCorrectlyRendersChangesTest(BrowserName browserName) =>
        StartHostAndTestPageAsync(
            TestHost,
            async (page, context) =>
            {
                var locator = page.Locator(HTMLSelectors.SidebarDrawer);
                await Assertions.Expect(locator).ToHaveClassAsync(MudDrawerOpenRegex());

                await page.ClickAsync(HTMLSelectors.NavDrawerToggleButton);
                await Assertions.Expect(locator).ToHaveClassAsync(MudDrawerClosedRegex());
            },
            browserName);

    [GeneratedRegex("mud-drawer--open")]
    private static partial Regex MudDrawerOpenRegex();

    [GeneratedRegex("mud-drawer--closed")]
    private static partial Regex MudDrawerClosedRegex();
}

file static class HTMLSelectors
{
    internal const string NavDrawerToggleButton = "#nav-toggle";
    internal const string SelectLanguageButton = "#select-language";
    internal const string SidebarDrawer = "#drawer";
}