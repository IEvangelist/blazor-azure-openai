// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Shared;

public sealed partial class NavMenu
{
    [Inject] public required IStringLocalizer<NavMenu> Localizer { get; set; }

    LocalizedString this[string name] => Localizer[name];
}
