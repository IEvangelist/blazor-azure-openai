// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Shared;

public record class AzureCulture
{
    public string Name { get; set; } = null!;
    public string NativeName { get; set; } = null!;
    public LanguageDirection Dir { get; set; }
}
