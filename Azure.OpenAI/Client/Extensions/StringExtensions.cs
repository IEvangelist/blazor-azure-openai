// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Extensions;

public static class StringExtensions
{
    private static readonly MarkdownPipeline s_pipeline = new MarkdownPipelineBuilder()
        .ConfigureNewLine("\n")
        .UseAdvancedExtensions()
        .UseEmojiAndSmiley()
        .UseSoftlineBreakAsHardlineBreak()
        .Build();

    public static string ToHtml(this string markdown) => string.IsNullOrWhiteSpace(markdown) is false
        ? Markdown.ToHtml(markdown, s_pipeline)
        : "";
}
