// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Pages;

public sealed partial class Chat
{
    string _userPrompt = "";
    bool _isReceivingResponse = false;

    string? _intermediateResponse = null;
    HashSet<string> _responses = new();

    readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
        .ConfigureNewLine("\n")
        .UseAdvancedExtensions()
        .UseEmojiAndSmiley()
        .UseSoftlineBreakAsHardlineBreak()
        .Build();

    [Inject] public required IStringLocalizer<Chat> Localizer { get; set; }

    string Prompt => Localizer[nameof(Prompt)];
    string ChatTitle => Localizer[nameof(ChatTitle)];
    string ChatPrompt => Localizer[nameof(ChatPrompt)];
    string Ask => Localizer[nameof(Ask)];

    void OnSendPrompt()
    {
        //if (_isReceivingResponse)
        //{
        //    return;
        //}
        //
        //_isReceivingResponse = true;
    }
}
