// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.EndToEndTests;

[CollectionDefinition(EndToEndTests)]
public class PlaywrightCollectionDefinition : ICollectionFixture<PlaywrightAsyncLifetime>
{
    public const string EndToEndTests = nameof(EndToEndTests);
}