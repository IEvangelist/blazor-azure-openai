// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.EndToEndTests;

// Pattern borrowed from:
// https://medium.com/younited-tech-blog/end-to-end-test-a-blazor-app-with-playwright-part-3-48c0edeff4b6
internal sealed class EndToEndHostFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // TODO: determine if we need to mock any APIs for our end-to-end testing.
        });

        var testHost = base.CreateHost(builder);

        builder.ConfigureWebHost(
          webHostBuilder => webHostBuilder.UseKestrel());

        var host = builder.Build();
        host.Start();

        return new CompositeHost(testHost, host);
    }
}

file sealed class CompositeHost : IHost
{
    private readonly IHost _testHost;
    private readonly IHost _kestrelHost;

    public CompositeHost(IHost testHost, IHost kestrelHost)
    {
        _testHost = testHost;
        _kestrelHost = kestrelHost;
    }

    IServiceProvider IHost.Services => _testHost.Services;

    void IDisposable.Dispose()
    {
        _testHost.Dispose();
        _kestrelHost.Dispose();
    }

    async Task IHost.StartAsync(CancellationToken cancellationToken)
    {
        await _testHost.StartAsync(cancellationToken);
        await _kestrelHost.StartAsync(cancellationToken);
    }

    async Task IHost.StopAsync(CancellationToken cancellationToken)
    {
        await _testHost.StopAsync(cancellationToken);
        await _kestrelHost.StopAsync(cancellationToken);
    }
}
