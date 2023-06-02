// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Models;

public readonly record struct GeneratedAnswer(
    string? Answer = null,
    string? Error = null)
{
    [JsonIgnore]
    public bool ContainsError => string.IsNullOrWhiteSpace(Error) is false;
}
