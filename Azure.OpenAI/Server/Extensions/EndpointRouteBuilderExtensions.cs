// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Server.Extensions;

internal static class EndpointRouteBuilderExtensions
{
    internal static IEndpointRouteBuilder MapAzureOpenAiApi(this IEndpointRouteBuilder routeBuilder)
    {
        var api = routeBuilder.MapGroup("api/openai");

        api.MapPost("chat", PostChatPromptAsync);
        api.MapPost("image", PostImagePromptAsync);

        return routeBuilder;
    }

    static async IAsyncEnumerable<TokenizedResponse> PostChatPromptAsync(
        OpenAIClient client, RequestPrompt prompt, IConfiguration config)
    {
        var deploymentId = config["AzureOpenAI:DeploymentId"];

        var messages = prompt.Persona.GetMessages();

        var response = await client.GetChatCompletionsStreamingAsync(
            new ChatCompletionsOptions
            {
                DeploymentName = deploymentId,
                Messages =
                {
                    // Set up our OpenAI ChatGPT persona.
                    new ChatMessage(ChatRole.System, messages.System),
                    new ChatMessage(ChatRole.User, messages.User),
                    new ChatMessage(ChatRole.Assistant,messages.Assistant),

                    // Feed user prompt into session.
                    new ChatMessage(ChatRole.User, prompt.Prompt)
                }
            });

        using var completions = response;

        await foreach (var update in completions)
        {
            yield return new TokenizedResponse(update.ContentUpdate);
        }
    }

    static async ValueTask<ImagesResponse> PostImagePromptAsync(
        OpenAIClient client, RequestPrompt prompt, IConfiguration config)
    {
        var deploymentId = config["AzureOpenAI:DeploymentId"];

        var response = await client.GetImageGenerationsAsync(new ImageGenerationOptions
        {
            Prompt = prompt.Prompt
        });

        var images = response.Value;

        return images switch
        {
            { Data.Count: > 0 } => new ImagesResponse(
                Created: images.Created,
                ImageURLs: images.Data.Select(static d => d.Url).ToArray()),

            _ => new ImagesResponse() { IsSuccessful = false }
        };
    }
}
