// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Services;

public sealed class CultureService(IHttpClientFactory factory, ILogger<CultureService> logger)
{
    internal async Task<IDictionary<CultureInfo, AzureCulture>> GetAvailableCulturesAsync()
    {
        using var client = factory.CreateClient();
        client.BaseAddress = new Uri("https://api.cognitive.microsofttranslator.com");

        var cultures = await client.GetFromJsonAsync<SharedCultures>(
            "languages?api-version=3.0&scope=translation",
            JsonSerializationDefaults.Options);

        if (cultures is null or { AvailableCultures.Count: 0 })
        {
            return new Dictionary<CultureInfo, AzureCulture>();
        }

        var azureCultures = cultures.AvailableCultures;

        var clientSupportedCultures =
            new HashSet<(CultureInfo Culture, AzureCulture AzureCulture)>();

        foreach (var group in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                    .GroupBy(culture => culture.TwoLetterISOLanguageName)
                    .Where(group => azureCultures.ContainsKey(group.Key)))
        {
            var azureCulture = azureCultures[group.Key];
            var culture = group.Key switch
            {
                "en" => group.Single(c => c.Name is "en-US"),
                "sv" => group.Single(c => c.Name is "sv-SE"),
                _ => null
            };

            if (culture is not null)
            {
                clientSupportedCultures.Add((culture, azureCulture));
                continue;
            }

            var cultureList = group.ToList();
            if (cultureList is { Count: 1 })
            {
                clientSupportedCultures.Add((cultureList[0], azureCulture));
                continue;
            }

            if (cultureList is { Count: > 1 })
            {
                var simpleCulture = $"{group.Key}-{group.Key.ToUpper()}";
                culture = group.SingleOrDefault(c => c.Name == simpleCulture)!;
                if (culture is not null)
                {
                    clientSupportedCultures.Add((culture, azureCulture));
                }
                else
                {
                    logger.LogInformation(
                        "Unable to find cultures for lang: {Key} - from: {Source}",
                        group.Key, string.Join(", ", cultureList.Select(c => c.Name)));
                }
            }
        }

        return clientSupportedCultures.ToDictionary(
            pair => pair.Culture,
            pair => pair.AzureCulture);
    }
}
