// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.EndToEndTests.Browser;

public class Host
{
    public string? Origin { get; set; }
    public LocalStorage[] LocalStorage { get; set; } = Array.Empty<LocalStorage>();
}
