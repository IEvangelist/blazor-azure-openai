using Azure.OpenAI.Client.Pages.VoiceChat;
using Microsoft.Extensions.Localization;

namespace Azure.OpenAI.Client.Components.VoiceDialog;

public sealed partial class VoiceDialog : IDisposable
{
    SpeechSynthesisVoice[] _voices = Array.Empty<SpeechSynthesisVoice>();
    readonly IList<double> _voiceSpeeds =
        Enumerable.Range(0, 12).Select(i => (i + 1) * .25).ToList();

    VoicePreferences? _voicePreferences;
    RequestVoiceState _state;

    [Inject] public required ISpeechSynthesisService SpeechSynthesis { get; set; }

    [Inject] public required ILocalStorageService LocalStorage { get; set; }

    [CascadingParameter] public required MudDialogInstance Dialog { get; set; }


    #region VoiceDialogLocalizer
    [Inject] public IStringLocalizer<VoiceDialog> Localizer { get; set; }

    public string ClientVoicesMsg => Localizer[nameof(ClientVoicesMsg)];
    public string Voice => Localizer[nameof(Voice)];
    public string VoiceSpeed => Localizer[nameof(VoiceSpeed)];
    public string TTPEnabled => Localizer[nameof(TTPEnabled)];
    public string LoadVoicesError => Localizer[nameof(LoadVoicesError)];
    public string cancel => Localizer[nameof(cancel)];
    public string Save => Localizer[nameof(Save)];

    #endregion \VoiceDialogLocalizer

    protected override async Task OnInitializedAsync()
    {
        _state = RequestVoiceState.RequestingVoices;

        await GetVoicesAsync();
        SpeechSynthesis.OnVoicesChanged(() => GetVoicesAsync(true));

        _voicePreferences = new VoicePreferences(LocalStorage);
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

    void SaveVoiceSelection() => Dialog.Close(DialogResult.Ok(_voicePreferences));

    void Cancel() => Dialog.Close(DialogResult.Ok(_voicePreferences));

    void IDisposable.Dispose() => SpeechSynthesis.UnsubscribeFromVoicesChanged();
}

internal enum RequestVoiceState
{
    RequestingVoices,
    FoundVoices,
    Error
}
