// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Models;

public record class TimelineItem(
    DateOnly Date,
    string Label,
    string Reference,
    Detail[] Details);

public record class Detail(
    string Feature,
    string? Reference = null);
