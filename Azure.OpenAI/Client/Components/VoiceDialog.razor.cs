using Azure.OpenAI.Client.Models;

namespace Azure.OpenAI.Client.Components;

public sealed partial class VoiceDialog : IDisposable
{
    const string PreferredVoiceKey = "preferred-voice";
    const string PreferredSpeedKey = "preferred-speed";

    SpeechSynthesisVoice[] _voices = Array.Empty<SpeechSynthesisVoice>();
    readonly IList<double> _voiceSpeeds =
        Enumerable.Range(0, 12).Select(i => (i + 1) * .25).ToList();
    double _voiceSpeed = 1.5;
    string? _selectedVoice;
    RequestVoiceState _state;

    [Inject] public required ISpeechSynthesisService SpeechSynthesis { get; set; }

    [Inject] public required ILocalStorageService LocalStorage { get; set; }

    [CascadingParameter] public required MudDialogInstance Dialog { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _state = RequestVoiceState.RequestingVoices;

        await GetVoicesAsync();
        SpeechSynthesis.OnVoicesChanged(() => GetVoicesAsync(true));

        if (LocalStorage.GetItem<string>(PreferredVoiceKey) is { Length: > 0 } voice)
        {
            _selectedVoice = voice;
        }
        if (LocalStorage.GetItem<double>(PreferredSpeedKey) is double speed && speed > 0)
        {
            _voiceSpeed = speed;
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

    void OnValueChanged(string args) => _selectedVoice = args;

    void SaveVoiceSelection()
    {
        LocalStorage.SetItem(PreferredVoiceKey, _selectedVoice);
        LocalStorage.SetItem(PreferredSpeedKey, _voiceSpeed);

        var voice = _voices.First(v => v.Name == _selectedVoice);

        Dialog.Close(DialogResult.Ok(
            new VoicePreferences(voice, _voiceSpeed)));
    }

    void Cancel() => Dialog.Close(DialogResult.Ok(
            new VoicePreferences(
                _voices.First(v => v.Name == _selectedVoice), _voiceSpeed)));

    void IDisposable.Dispose() => SpeechSynthesis.UnsubscribeFromVoicesChanged();
}

internal enum RequestVoiceState
{
    RequestingVoices,
    FoundVoices,
    Error
}
