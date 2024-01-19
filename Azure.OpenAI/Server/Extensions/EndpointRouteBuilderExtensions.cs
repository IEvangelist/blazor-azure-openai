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
        OpenAIClient client,
        RequestPrompt prompt,
        IConfiguration config,
        [EnumeratorCancellation] CancellationToken cancellationToken)
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
                    new ChatRequestSystemMessage(messages.System),
                    new ChatRequestUserMessage(messages.User),
                    new ChatRequestAssistantMessage(messages.Assistant),

                    // Feed user prompt into session.
                    new ChatRequestUserMessage(prompt.Prompt)
                }
            },
            cancellationToken);

        await foreach (var choice in response.EnumerateValues())
        {
            yield return new TokenizedResponse(choice.ContentUpdate);
        }
    }

    static async ValueTask<ImagesResponse> PostImagePromptAsync(
        OpenAIClient client,
        RequestPrompt prompt,
        IConfiguration config,
        CancellationToken cancellationToken)
    {
        var deploymentId = config["AzureOpenAI:DeploymentId"];

        var response = await client.GetImageGenerationsAsync(
            new ImageGenerationOptions
            {
                Prompt = prompt.Prompt
            },
            cancellationToken);

        var images = response.Value;

        return images switch
        {
            { Data.Count: > 0 } => new ImagesResponse(
                Created: images.Created,
                ImageURLs: [..images.Data.Select(static d => d.Url)]),

            _ => new ImagesResponse() { IsSuccessful = false }
        };
    }
}
