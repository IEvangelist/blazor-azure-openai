// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Interop;

internal sealed partial class JavaScriptModule
{
    [JSImport("scrollIntoView", nameof(JavaScriptModule))]
    public static partial Task ScrollIntoView(string id);
}
