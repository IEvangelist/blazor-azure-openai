// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Models;

public readonly record struct TimelineItem(
    DateOnly Date,
    string Label,
    string Reference,
    Detail[] Details);

public readonly record struct Detail(
    string Feature,
    string? Reference = null);
