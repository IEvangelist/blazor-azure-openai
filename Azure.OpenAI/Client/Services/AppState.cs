// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Services;

public sealed class AppState
{
    public event Action? OnDeleteClicked;

    public void DeleteClicked() => OnDeleteClicked?.Invoke();
}
