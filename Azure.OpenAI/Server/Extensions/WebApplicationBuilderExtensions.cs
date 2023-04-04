// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Server.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static WebApplicationBuilder AddAzureOpenAI(this WebApplicationBuilder app)
    {
        app.Services.AddAzureClients(
            factory =>
            {
                var endpoint = Environment.GetEnvironmentVariable(
                    "AzureOpenAI__Endpoint");
                ArgumentNullException.ThrowIfNull(endpoint);

                var apiKey = Environment.GetEnvironmentVariable(
                    "AzureOpenAI__ApiKey");
                ArgumentNullException.ThrowIfNull(apiKey);

                factory.AddOpenAIClient(
                    new Uri(endpoint), new AzureKeyCredential(apiKey));
            });

        return app;
    }
}
