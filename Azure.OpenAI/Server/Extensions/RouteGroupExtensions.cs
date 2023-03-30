using Azure.OpenAI.Shared;

namespace Azure.OpenAI.Server.Extensions;

internal static class RouteGroupExtensions
{
    internal static RouteGroupBuilder MapOpenAI(this RouteGroupBuilder openAI)
    {
        openAI.MapPost("chat", OnChatPromptPost);

        return openAI;
    }

    static async IAsyncEnumerable<string> OnChatPromptPost(OpenAIClient client, [FromBody] ChatPrompt prompt)
    {
        var response = await client.GetChatCompletionsStreamingAsync(
            "pine-chat", new ChatCompletionsOptions
            {
                Messages =
                {
                    new ChatMessage(ChatRole.Assistant, """
                        You're an AI assistant for developers, helping them write code more efficiently.
                        You're name is 'Blazor Clippy'.
                        You will always reply with a Markdown formatted response.
                        """),

                    new ChatMessage(ChatRole.User, prompt.Prompt)
                }
            });

        using StreamingChatCompletions completions = response.Value;
        await foreach (var choice in completions.GetChoicesStreaming())
        {
            await foreach (var message in choice.GetMessageStreaming())
            {
                yield return message.Content;
            }
        }
    }
}
