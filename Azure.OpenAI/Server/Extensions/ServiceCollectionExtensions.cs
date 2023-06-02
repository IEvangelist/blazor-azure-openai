// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Server.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddAzureOpenAI(this IServiceCollection services, IConfiguration config)
    {
        services.AddAzureClients(
            factory =>
            {
                var endpoint = config["AzureOpenAI:Endpoint"];
                ArgumentNullException.ThrowIfNull(endpoint);

                var apiKey = config["AzureOpenAI:ApiKey"];
                ArgumentNullException.ThrowIfNull(apiKey);

                factory.AddOpenAIClient(
                    new Uri(endpoint), new AzureKeyCredential(apiKey));
            });

        return services;
    }
}
