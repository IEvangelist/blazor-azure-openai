// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Components;

public sealed partial class Assistant
{
    [Inject] public required IStringLocalizer<Assistant> Localizer { get; set; }
}
