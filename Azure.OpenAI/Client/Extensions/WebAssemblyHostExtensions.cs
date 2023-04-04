// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Extensions;

internal static class WebAssemblyHostExtensions
{
    internal static WebAssemblyHost DetectClientCulture(this WebAssemblyHost host)
    {
        var localStorage = host.Services.GetRequiredService<ILocalStorageService>();
        var clientCulture = localStorage.GetItem<string>("blazor-openai-client-culture");
        clientCulture ??= "en";

        CultureInfo culture = new(clientCulture);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        return host;
    }
}
