// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace Azure.OpenAI.Shared;

public record class SharedCultures
{
    [JsonPropertyName("translation")]
    public required IDictionary<string, AzureCulture> AvailableCultures { get; set; }
}
