// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Components;

public sealed partial class AnswerError
{
    [Parameter, EditorRequired] public required UserQuestion Question { get; set; }
    [Parameter, EditorRequired] public required string Error { get; set; }
    [Parameter, EditorRequired] public required EventCallback<UserQuestion> OnRetryClicked { get; set; }

    private async Task OnRetryClickedAsync()
    {
        if (OnRetryClicked.HasDelegate)
        {
            await OnRetryClicked.InvokeAsync(Question);
        }
    }
}
