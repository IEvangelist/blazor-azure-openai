﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Services;

public sealed partial class OpenAIPromptQueue(
        IServiceProvider provider,
        ILogger<OpenAIPromptQueue> logger,
        ObjectPool<StringBuilder> builderPool)
{
    Task? _processPromptTask = null;

    public void Enqueue(string prompt, AssistantPersona persona, Func<PromptResponse, Task> handler)
    {
        if (_processPromptTask is not null)
        {
            return;
        }

        _processPromptTask = Task.Run(async () =>
        {
            var responseBuffer = builderPool.Get();
            responseBuffer.Clear(); // Ensure initial state is empty.

            var isError = false;
            var debugLogEnabled = logger.IsEnabled(LogLevel.Debug);

            try
            {
                using var scope = provider.CreateScope();
                using var client = scope.ServiceProvider.GetRequiredService<HttpClient>();

                var options = JsonSerializationDefaults.Options;
                var chatPrompt = new RequestPrompt { Prompt = prompt, Persona = persona };
                var json = chatPrompt.ToJson(options);
                using var body = new StringContent(
                    json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(
                    "api/openai/chat", body);
                response.EnsureSuccessStatusCode();

                using var stream = await response.Content.ReadAsStreamAsync();

                await foreach (var tokenizedResponse in
                    JsonSerializer.DeserializeAsyncEnumerable<TokenizedResponse>(
                        stream, options))
                {
                    if (tokenizedResponse is null)
                    {
                        continue;
                    }

                    responseBuffer.Append(tokenizedResponse.Content);

                    var responseText = NormalizeResponseText(
                        responseBuffer, logger, debugLogEnabled);

                    await handler(
                        new PromptResponse(
                            prompt, responseText, false));

                    // Required for Blazor to render live updates.
                    await Task.Delay(1);
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(
                    ex,
                    "Unable to generate response: {Error}",
                    ex.Message);

                await handler(
                    new PromptResponse(
                        prompt, ex.Message, true, isError = true));
            }
            finally
            {
                if (isError is false)
                {
                    var responseText = NormalizeResponseText(
                        responseBuffer, logger, debugLogEnabled);

                    await handler(
                        new PromptResponse(
                            prompt, responseText, true));
                }

                builderPool.Return(responseBuffer);
                _processPromptTask = null;
            }
        });
    }

    private static string NormalizeResponseText(
        StringBuilder builder, ILogger logger, bool debugLogEnabled)
    {
        if (builder is null or { Length: 0 })
        {
            return "";
        }

        var text = builder.ToString();

        if (debugLogEnabled)
        {
            logger.LogDebug(
                "Before normalize:{Newline}{Tab}{Text}",
                Environment.NewLine, '\t', text);
        }

        text = LineEndingsRegex().Replace(text, "\n");
        text = Regex.Unescape(text);

        if (debugLogEnabled)
        {
            logger.LogDebug(
                "After normalize:{Newline}{Tab}{Text}",
                Environment.NewLine, '\t', text);
        }

        return text;
    }

    [GeneratedRegex("\\r\\n|\\n\\r|\\r")]
    private static partial Regex LineEndingsRegex();
}