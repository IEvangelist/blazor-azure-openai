// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Models;

public record class QuestionAndAnswer(
    UserQuestion Question,
    GeneratedAnswer? Answer = null);
