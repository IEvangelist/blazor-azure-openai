// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using Azure.OpenAI.Client.Models;
using static MudBlazor.CategoryTypes;

namespace Azure.OpenAI.Client.Components;

public sealed partial class UserPrompt : IDisposable
{
    private string _userQuestion = "";
    private UserQuestion _currentQuestion;
    private bool _isRecognizingSpeech = false;
    private bool _isReceivingResponse = false;
    private bool _isReadingResponse = false;
    private VoicePreferences? _voicePreferences;
    private IDisposable? _recognitionSubscription;

    [Parameter, EditorRequired]
    public required EventCallback<PromptQuestion> OnPromptSubmitted { get;set; }

    [Parameter, EditorRequired]
    public required EventCallback OnStopTalking { get; set; }

    [Inject] public required IStringLocalizer<UserPrompt> Localizer { get; set; }
    [Inject] public required ISpeechRecognitionService SpeechRecognition { get; set; }
    [Inject] public required ILogger<UserPrompt> Logger { get; set; }

    [Inject] public required IDialogService Dialog { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SpeechRecognition.InitializeModuleAsync();
        }
    }

    private async Task OnSendPrompt()
    {
        if (OnPromptSubmitted is { HasDelegate: true } handler)
        {
            await handler.InvokeAsync((_userQuestion, false));
        }
    }

    private async Task ShowVoiceDialogAsync()
    {
        var dialog = await Dialog.ShowAsync<VoiceDialog>(title: "🔊 Text-to-speech Preferences");
        var result = await dialog.Result;
        if (result is not { Canceled: true })
        {
            _voicePreferences = await dialog.GetReturnValueAsync<VoicePreferences>();
        }
    }

    private async Task OnKeyUp(KeyboardEventArgs args)
    {
        if (args is { Key: "Enter", ShiftKey: false } &&
            OnPromptSubmitted is { HasDelegate: true } handler)
        {
            await handler.InvokeAsync((_userQuestion, false));
        }
    }

    private async Task StopTalking()
    {
        if (OnStopTalking is { HasDelegate: true} handler)
        {
            await handler.InvokeAsync();
        }
    }

    private void OnRecognizeSpeechClick()
    {
        if (_isRecognizingSpeech)
        {
            SpeechRecognition.CancelSpeechRecognition(false);
        }
        else
        {
            var bcp47Tag = CultureInfo.CurrentUICulture.Name;

            _recognitionSubscription?.Dispose();
            _recognitionSubscription = SpeechRecognition.RecognizeSpeech(
                bcp47Tag,
                OnRecognized,
                OnError,
                OnStarted,
                OnEnded);
        }
    }

    private void OnStarted()
    {
        _isRecognizingSpeech = true;
        StateHasChanged();
    }

    private void OnEnded()
    {
        _isRecognizingSpeech = false;
        StateHasChanged();
    }

    private void OnError(SpeechRecognitionErrorEvent errorEvent)
    {
        Logger.LogWarning(
            "{Error}: {Message}",
            errorEvent.Error, errorEvent.Message ?? "Error message, unknown.");

        StateHasChanged();
    }

    private void OnRecognized(string transcript)
    {
        _userQuestion = _userQuestion switch
        {
            null => transcript,
            _ => $"{_userQuestion.Trim()} {transcript}".Trim()
        };

        StateHasChanged();
    }

    void IDisposable.Dispose() => _recognitionSubscription?.Dispose();
}
