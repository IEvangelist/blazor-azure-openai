// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Models;

public readonly record struct UserQuestion(
    string Question,
    DateTime AskedOn);
