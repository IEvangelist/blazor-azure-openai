// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;

namespace Azure.OpenAI.Shared;

public record class ImagesResponse(
    DateTimeOffset Created = default,
    [property: MemberNotNullWhen(true, "IsSuccessful")]
    Uri[]? ImageURLs = null)
{
    public bool IsSuccessful { get; init; } = true;
}
