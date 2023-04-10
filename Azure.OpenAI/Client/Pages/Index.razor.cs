// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Pages;

public sealed partial class Index
{
    [Inject] public required IStringLocalizer<Index> Localizer { get; set; }

    string HomeTitle => Localizer[nameof(HomeTitle)];
    string BingImageText => Localizer[nameof(BingImageText)];
    string BingImageCreatorLinkText => Localizer[nameof(BingImageCreatorLinkText)];
    string AzureSdkGitHubLinkTitle => Localizer[nameof(AzureSdkGitHubLinkTitle)];
    string NuGetLinkAzureOpenAI => Localizer[nameof(NuGetLinkAzureOpenAI)];
    string MicrosoftLearnContentLinkTitle => Localizer[nameof(MicrosoftLearnContentLinkTitle)];
    string MudBlazorLink => Localizer[nameof(MudBlazorLink)];
}
