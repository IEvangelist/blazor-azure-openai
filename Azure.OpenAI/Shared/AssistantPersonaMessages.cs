// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Shared;

/// <summary>
/// The persona messages to use for the assistant's output.
/// </summary>
/// <param name="System">
/// `ChatRole.System` specifies the instructions for the system.
/// </param>
/// <param name="User">
/// `ChatRole.User` specifies example user input.
/// </param>
/// <param name="Assistant">
/// `ChatRole.Assistant` specifies an example response.
/// </param>
public readonly record struct AssistantPersonaMessages(
    string System,
    string User,
    string Assistant);
