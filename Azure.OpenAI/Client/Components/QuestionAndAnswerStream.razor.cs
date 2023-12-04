// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Components;

public sealed partial class QuestionAndAnswerStream
{
    [Parameter, EditorRequired]
    public required Dictionary<DateTime, QuestionAndAnswer> QuestionAndAnswerMap { get; set; } = new();

    [Parameter, EditorRequired]
    public required EventCallback<PromptQuestion> OnAskQuestion { get; set; }

    [Inject]
    public required IStringLocalizer<QuestionAndAnswerStream> Localizer { get; set; }

    private async Task OnAskQuestionAsync(UserQuestion userQuestion)
    {
        if (OnAskQuestion is { HasDelegate: true } handler)
        {
            await handler.InvokeAsync((userQuestion.Question, true));
        }
    }
}
