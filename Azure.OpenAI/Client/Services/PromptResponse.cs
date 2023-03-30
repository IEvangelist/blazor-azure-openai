namespace Azure.OpenAI.Client.Services;

public record PromptResponse(string Prompt, string Response, bool IsComplete = false);
