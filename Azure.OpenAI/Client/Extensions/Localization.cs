// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Extensions;

internal sealed class Localization
{

    internal  static async Task ConfigCulture(WebAssemblyHost host)
    {
        var localStorage = host.Services.GetRequiredService<ILocalStorageService>();
        var clientCulture = localStorage.GetItem<string>("blazor-openai-client-culture");
        clientCulture ??= "en";

        CultureInfo culture = new(clientCulture);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}
