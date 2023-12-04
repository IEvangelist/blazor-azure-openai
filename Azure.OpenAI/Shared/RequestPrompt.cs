// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Shared;

public class RequestPrompt
{
    public string Prompt { get; set; } = default!;

    public AssistantPersona Persona { get; set; } = AssistantPersona.Clippy;
}
