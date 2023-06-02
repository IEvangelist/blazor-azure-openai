// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure.OpenAI.Shared;

public static class JsonSerializationDefaults
{
    /// <summary>
    /// Gets the apps shared <see cref="JsonSerializerOptions"/> instance.
    /// Constructed using the <see cref="JsonSerializerDefaults.Web"/> defaults.
    /// </summary>
    public static JsonSerializerOptions Options { get; } = new(JsonSerializerDefaults.Web)
    {
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };
}
