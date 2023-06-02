// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Server.Extensions;

internal static class EndpointRouteBuilderExtensions
{
    internal static IEndpointRouteBuilder MapAzureOpenAiApi(this IEndpointRouteBuilder routeBuilder)
    {
        var api = routeBuilder.MapGroup("api/openai");

        api.MapPost("chat", PostChatPromptAsync);

        return routeBuilder;
    }

    static async IAsyncEnumerable<TokenizedResponse> PostChatPromptAsync(
        OpenAIClient client, ChatPrompt prompt, IConfiguration config)
    {
        var deploymentId = config["AzureOpenAI:DeploymentId"] ?? "pine-chat";

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
}
