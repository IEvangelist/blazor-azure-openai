// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Services;

public sealed class OpenAIPromptQueue
{
    readonly IServiceProvider _provider;
    readonly ILogger<OpenAIPromptQueue> _logger;
    readonly StringBuilder _responseBuffer = new();
    Task? _processPromptTask = null;

    public OpenAIPromptQueue(IServiceProvider provider, ILogger<OpenAIPromptQueue> logger) =>
        (_provider, _logger) = (provider, logger);

    public void Enqueue(string prompt, Func<PromptResponse, Task> handler)
    {
        if (_processPromptTask is not null)
        {
            return;
        }

        _processPromptTask = Task.Run(async () =>
        {
            try
            {
                var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                var json = new ChatPrompt { Prompt = prompt }.ToJson(options);

                using var body = new StringContent(json, Encoding.UTF8, "application/json");
                using var scope = _provider.CreateScope();

                var factory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
                using var client = scope.ServiceProvider.GetRequiredService<HttpClient>();

                var response = await client.PostAsync("api/openai/chat", body);
                if (response.IsSuccessStatusCode)
                {
                    using var stream = await response.Content.ReadAsStreamAsync();

                    await foreach (var tokenizedResponse in
                        JsonSerializer.DeserializeAsyncEnumerable<TokenizedResponse>(stream, options))
                    {
                        if (tokenizedResponse is null)
                        {
                            continue;
                        }

                        _responseBuffer.Append(tokenizedResponse.Content);

                        var responseText = NormalizeResponseText(_responseBuffer, _logger);
                        await handler(
                            new PromptResponse(
                                prompt, responseText, false));

                        await Task.Delay(1);
                    }
                }
            }
            catch (Exception ex)
            {
                await handler(
                    new PromptResponse(prompt, ex.Message, true));
            }
            finally
            {
                var responseText = NormalizeResponseText(_responseBuffer, _logger);
                await handler(
                    new PromptResponse(
                        prompt, responseText, true));

                _responseBuffer.Clear();
                _processPromptTask = null;
            }
        });
    }

    private static string NormalizeResponseText(StringBuilder builder, ILogger logger)
    {
        if (builder is null or { Length: 0 })
        {
            return "";
        }

        var text = builder.ToString();

        logger.LogDebug("Before normalize\n\t{Text}", text);

        text = text.Replace("\r", "\n")
            .Replace("\\n\\r", "\n")
            .Replace("\\n", "\n");

        text = Regex.Unescape(text);

        logger.LogDebug("After normalize:\n\t{Text}", text);

        return text;
    }
}
