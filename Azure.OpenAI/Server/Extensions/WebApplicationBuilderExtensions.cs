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
                var endpoint = app.Configuration["AzureOpenAI:Endpoint"];
                ArgumentNullException.ThrowIfNull(endpoint);

                var apiKey = app.Configuration["AzureOpenAI:ApiKey"];
                ArgumentNullException.ThrowIfNull(apiKey);

                factory.AddOpenAIClient(
                    new Uri(endpoint), new AzureKeyCredential(apiKey));
            });

        return app;
    }
}
