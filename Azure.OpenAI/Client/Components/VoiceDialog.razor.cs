// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Components;

public sealed partial class VoiceDialog : IDisposable
{
    SpeechSynthesisVoice[] _voices = Array.Empty<SpeechSynthesisVoice>();
    readonly IList<double> _voiceSpeeds =
        Enumerable.Range(0, 12).Select(i => (i + 1) * .25).ToList();

    VoicePreferences? _voicePreferences;
    RequestVoiceState _state;

    [Inject] public required ISpeechSynthesisService SpeechSynthesis { get; set; }

    [Inject] public required ILocalStorageService LocalStorage { get; set; }

    [Inject] public required IStringLocalizer<VoiceDialog> Localizer { get; set; }

    [CascadingParameter] public required MudDialogInstance Dialog { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _state = RequestVoiceState.RequestingVoices;

        await GetVoicesAsync();
        SpeechSynthesis.OnVoicesChanged(() => GetVoicesAsync(true));

        _voicePreferences = new VoicePreferences(LocalStorage);

        if (_voicePreferences.Voice is null &&
            _voices.FirstOrDefault(voice => voice.Default) is { } voice)
        {
            _voicePreferences.Voice = voice.Name;
        }
    }

    async Task GetVoicesAsync(bool isFromCallback = false)
    {
        _voices = await SpeechSynthesis.GetVoicesAsync();
        if (_voices is { } && isFromCallback)
        {
            StateHasChanged();
        }

        if (_voices is { Length: > 0 })
        {
            _state = RequestVoiceState.FoundVoices;
        }
    }

    void OnValueChanged(string selectedVoice) => _voicePreferences = _voicePreferences! with
    {
        Voice = selectedVoice
    };

    void OnSaveVoiceSelection() => Dialog.Close(DialogResult.Ok(_voicePreferences));

    void OnCancel() => Dialog.Close(DialogResult.Ok(_voicePreferences));

    void IDisposable.Dispose() => SpeechSynthesis.UnsubscribeFromVoicesChanged();
}

internal enum RequestVoiceState
{
    RequestingVoices,
    FoundVoices,
    Error
}
