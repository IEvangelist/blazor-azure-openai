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

        // TODO: take on different persona, given the ChatPrompt...

        var response = await client.GetChatCompletionsStreamingAsync(
            deploymentId, new ChatCompletionsOptions
            {
                Messages =
                {
                    new ChatMessage(ChatRole.System, """
                        You're an AI assistant for developers, helping them write code more efficiently.
                        You're name is "Blazor Clippy".
                        You will always reply with a Markdown formatted response.
                        """),

                    new ChatMessage(ChatRole.User, "What's your name?"),

                    new ChatMessage(ChatRole.Assistant,
                        "Hi, my name is **Blazor Clippy**! Nice to meet you. 🤓"),

                    new ChatMessage(ChatRole.User, prompt.Prompt)
                }
            });

        using var completions = response.Value;
        await foreach (var choice in completions.GetChoicesStreaming())
        {
            await foreach (var message in choice.GetMessageStreaming())
            {
                yield return new TokenizedResponse(message.Content);
            }
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
