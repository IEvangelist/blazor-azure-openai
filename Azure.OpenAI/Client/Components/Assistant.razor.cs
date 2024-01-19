// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Components;

public sealed partial class Assistant
{
    [Inject] public required IStringLocalizer<Assistant> Localizer { get; set; }

    [Parameter, EditorRequired]
    public AssistantPersona Persona { get; set; }

    private string PersonaName => Persona switch
    {
        AssistantPersona.Pirate => "Blazor Black Beard 🦜",
        AssistantPersona.Clippy => "Blazor 📎 Clippy",
        AssistantPersona.Yoda => "Blazor 🥋 Yoda",
        _ => "Blazor 🤖 Bot"
    };
}
