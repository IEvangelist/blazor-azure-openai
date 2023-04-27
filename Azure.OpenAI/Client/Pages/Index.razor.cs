// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Pages;

public sealed partial class Index
{
    [Inject] public required IStringLocalizer<Index> Localizer { get; set; }
}
