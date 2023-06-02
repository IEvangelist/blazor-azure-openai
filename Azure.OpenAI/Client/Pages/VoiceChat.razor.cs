// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Pages;

public sealed partial class VoiceChat : IDisposable
{
    private const string AnswerElementId = "replies";

    private DateTime _askedOn;
    private string _userQuestion = "";
    private UserQuestion _currentQuestion;
    private bool _isRecognizingSpeech = false;
    private bool _isReceivingResponse = false;
    private bool _isReadingResponse = false;
    private IDisposable? _recognitionSubscription;
    private VoicePreferences? _voicePreferences;
    private Dictionary<DateTime, QuestionAndAnswer> _questionAndAnswerMap = new();

    [Inject] public required OpenAIPromptQueue OpenAIPrompts { get; set; }
    [Inject] public required IDialogService Dialog { get; set; }
    [Inject] public required ISpeechRecognitionService SpeechRecognition { get; set; }
    [Inject] public required ISpeechSynthesisService SpeechSynthesis { get; set; }
    [Inject] public required ILocalStorageService LocalStorage { get; set; }
    [Inject] public required ISessionStorageService SessionStorage { get; set; }
    [Inject] public required IJSInProcessRuntime JavaScript { get; set; }
    [Inject] public required ILogger<VoiceChat> Logger { get; set; }
    [Inject] public required IStringLocalizer<VoiceChat> Localizer { get; set; }
    [Inject] public required AppState State { get; set; }

    protected override void OnInitialized()
    {
        if (SessionStorage.GetItem<Dictionary<DateTime, QuestionAndAnswer>>(StorageKeys.SessionChatHistoryKey)
            is { } map)
        {
            if (map is null or { Count: 0 })
            {
                return;
            }

            _questionAndAnswerMap = map;

            _ = Task.Delay(TimeSpan.FromSeconds(.2))
                    .ContinueWith(
                        _ => JavaScriptModule.ScrollIntoView(AnswerElementId));
        }

        State.OnDeleteHistoryClicked += OnDeleteHistoryClick;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SpeechRecognition.InitializeModuleAsync();
        }
    }

    private Task OnAskQuestionAsync(UserQuestion userQuestion)
    {
        _currentQuestion = userQuestion;        

        OnSendPrompt(true);

        return Task.CompletedTask;
    }

    private void OnSendPrompt(bool repeatQuestion = false)
    {
        if (_isReceivingResponse || string.IsNullOrWhiteSpace(_userQuestion))
        {
            return;
        }

        _isReceivingResponse = true;
        if (repeatQuestion is false)
        {
            _askedOn = DateTime.Now;
            _currentQuestion = new(_userQuestion, _askedOn);
        }

        _questionAndAnswerMap[_askedOn] = new QuestionAndAnswer(_currentQuestion);

        OpenAIPrompts.Enqueue(
            _userQuestion,
            async (PromptResponse response) => await InvokeAsync(() =>
            {
                var (_, responseText, isComplete, isError) = response;
                var html = responseText.ToHtml();

                _questionAndAnswerMap[_askedOn] = _questionAndAnswerMap[_askedOn] with
                {
                    Answer = isError ? new(Answer: null, Error: html) : new(Answer: html)
                };

                SessionStorage.SetItem(StorageKeys.SessionChatHistoryKey, _questionAndAnswerMap);

                _isReceivingResponse = isComplete is false;
                if (isComplete && isError is false)
                {
                    TrySpeakResponse(responseText);
                    ResetState();
                }

                JavaScriptModule.ScrollIntoView(AnswerElementId);

                StateHasChanged();
            }));
    }

    private void TrySpeakResponse(string responseText)
    {
        _voicePreferences = new VoicePreferences(LocalStorage);
        var (voice, rate, isEnabled) = _voicePreferences;
        if (isEnabled)
        {
            _isReadingResponse = true;
            var utterance = new SpeechSynthesisUtterance
            {
                Rate = rate,
                Text = responseText
            };

            if (voice is not null)
            {
                utterance.Voice = new SpeechSynthesisVoice
                {
                    Name = voice
                };
            }

            SpeechSynthesis.Speak(utterance, duration =>
            {
                _isReadingResponse = false;
                StateHasChanged();
            });
        }
    }

    private void ResetState()
    {
        _userQuestion = "";
        _currentQuestion = default;
    }

    private void OnDeleteHistoryClick()
    {
        _userQuestion = "";
        _currentQuestion = default;
        _questionAndAnswerMap.Clear();

        SessionStorage.RemoveItem(StorageKeys.SessionChatHistoryKey);
        StateHasChanged();
    }

    private void OnKeyUp(KeyboardEventArgs args)
    {
        if (args is { Key: "Enter", ShiftKey: false })
        {
            OnSendPrompt();
        }
    }

    protected override void OnAfterRender(bool firstRender) =>
        JavaScript.InvokeVoid("highlight");

    private void StopTalking()
    {
        SpeechSynthesis.Cancel();
        _isReadingResponse = false;
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

    private async Task ShowVoiceDialogAsync()
    {
        var dialog = await Dialog.ShowAsync<VoiceDialog>(title: "🔊 Text-to-speech Preferences");
        var result = await dialog.Result;
        if (result is not { Canceled: true })
        {
            _voicePreferences = await dialog.GetReturnValueAsync<VoicePreferences>();
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

    public void Dispose()
    {
        _recognitionSubscription?.Dispose();

        State.OnDeleteHistoryClicked -= OnDeleteHistoryClick;
    }
}
