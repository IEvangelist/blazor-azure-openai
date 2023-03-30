using Azure.OpenAI.Client.Components;
using Azure.OpenAI.Client.Models;

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
    VoicePreferences _voicePreferences;
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
    [Inject] public required ISessionStorageService SessionStorage { get; set; }
    [Inject] public required IJSInProcessRuntime JavaScript { get; set; }

    protected override void OnInitialized()
    {
        if (SessionStorage.GetItem<HashSet<string>>("openai-prompt-responses") is
            {
                Count: > 0
            } responses)
        {
            _responses = responses;
        }
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

        OpenAIPrompts.Enqueue(
            _userPrompt,
            async response => await InvokeAsync(async () =>
        {
            var (_, responseText, isComplete) = response;
            var html = Markdown.ToHtml(responseText, _pipeline);

            _intermediateResponse = html;

            if (isComplete)
            {
                _responses.Add(_intermediateResponse);
                SessionStorage.SetItem("openai-prompt-responses", _responses);

                _intermediateResponse = null;

                _isReadingResponse = true;
                await JavaScript.InvokeVoidAsync("highlight");

                if (_voicePreferences.Equals(default))
                {
                    _voicePreferences = new(new SpeechSynthesisVoice
                    {
                        Name = "Microsoft Aria Online (Natural) - English (United States)"
                    }, 1.5);
                }

                var (voice, rate) = _voicePreferences;

                SpeechSynthesis.Speak(new SpeechSynthesisUtterance
                {
                    Rate = rate,
                    Text = responseText,
                    Voice = voice
                }, duration => _isReadingResponse = false);
            }

            _isReceivingResponse = isComplete is false;
            if (isComplete)
            {
                _userPrompt = "";
            }

            StateHasChanged();
        }));
    }

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
        var dialog = await Dialog.ShowAsync<VoiceDialog>("Text-to-speech Preferences");
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
