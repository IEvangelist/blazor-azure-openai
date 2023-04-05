// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Pages;

public sealed partial class VoiceChat : IDisposable
{
    string _userPrompt = "";
    bool _isRecognizingSpeech = false;
    bool _isReceivingResponse = false;
    bool _isReadingResponse = false;

    string? _intermediateResponse = null;
    IDisposable? _recognitionSubscription;
    SpeechRecognitionErrorEvent? _errorEvent;
    VoicePreferences? _voicePreferences;
    HashSet<string> _responses = new();

    readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
        .ConfigureNewLine("\n")
        .UseAdvancedExtensions()
        .UseEmojiAndSmiley()
        .UseSoftlineBreakAsHardlineBreak()
        .Build();

    [Inject] public required OpenAIPromptQueue OpenAIPrompts { get; set; }
    [Inject] public required IDialogService Dialog { get; set; }
    [Inject] public required ISpeechRecognitionService SpeechRecognition { get; set; }
    [Inject] public required ISpeechSynthesisService SpeechSynthesis { get; set; }
    [Inject] public required ILocalStorageService LocalStorage { get; set; }
    [Inject] public required ISessionStorageService SessionStorage { get; set; }
    [Inject] public required IJSInProcessRuntime JavaScript { get; set; }
    [Inject] public required IStringLocalizer<VoiceChat> Localizer { get; set; }

    string Prompt => Localizer[nameof(Prompt)];
    string Save => Localizer[nameof(Save)];
    string Speak => Localizer[nameof(Speak)];
    string Stop => Localizer[nameof(Stop)];
    string Chat => Localizer[nameof(Chat)];
    string ChatPrompt => Localizer[nameof(ChatPrompt)];
    string Ask => Localizer[nameof(Ask)];
    string TTSPreferences => Localizer[nameof(TTSPreferences)];

    protected override void OnInitialized()
    {
        if (SessionStorage.GetItem<HashSet<string>>("openai-prompt-responses") is
            {
                Count: > 0
            } responses)
        {
            _responses = responses;
        }
        _voicePreferences = new VoicePreferences(LocalStorage);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SpeechRecognition.InitializeModuleAsync();
        }
    }

    void OnSendPrompt()
    {
        if (_isReceivingResponse)
        {
            return;
        }

        _isReceivingResponse = true;

        OpenAIPrompts.Enqueue(
            _userPrompt,
            async response => await InvokeAsync(() =>
        {
            var (_, responseText, isComplete) = response;
            var promptWithResponseText = $"""
                > {_userPrompt}

                {responseText}
                """;
            var html = Markdown.ToHtml(promptWithResponseText, _pipeline);

            _intermediateResponse = html;

            if (isComplete)
            {
                _responses.Add(_intermediateResponse);
                SessionStorage.SetItem("openai-prompt-responses", _responses);

                _intermediateResponse = null;
                _isReadingResponse = true;

                var (voice, rate, isEnabled) = _voicePreferences!;
                if (isEnabled)
                {
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

            _isReceivingResponse = isComplete is false;
            if (isComplete)
            {
                _userPrompt = "";
            }

            StateHasChanged();
        }));
    }

    protected override void OnAfterRender(bool firstRender) =>
        JavaScript.InvokeVoid("highlight");

    void StopTalking()
    {
        SpeechSynthesis.Cancel();
        _isReadingResponse = false;
    }

    void OnRecognizeSpeechClick()
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

    async Task ShowVoiceDialog()
    {
        var dialog = await Dialog.ShowAsync<VoiceDialog>(title: TTSPreferences);
        var result = await dialog.Result;
        if (result is not { Canceled: true })
        {
            _voicePreferences = await dialog.GetReturnValueAsync<VoicePreferences>();
        }
    }

    void OnStarted()
    {
        _isRecognizingSpeech = true;
        StateHasChanged();
    }

    void OnEnded()
    {
        _isRecognizingSpeech = false;
        StateHasChanged();
    }

    void OnError(SpeechRecognitionErrorEvent errorEvent)
    {
        _errorEvent = errorEvent;
        StateHasChanged();
    }

    void OnRecognized(string transcript)
    {
        _userPrompt = _userPrompt switch
        {
            null => transcript,
            _ => $"{_userPrompt.Trim()} {transcript}".Trim()
        };

        StateHasChanged();
    }

    void IDisposable.Dispose() => _recognitionSubscription?.Dispose();
}
