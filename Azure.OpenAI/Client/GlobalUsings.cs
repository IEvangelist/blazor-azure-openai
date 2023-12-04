// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

global using System.Globalization;
global using System.Net.Http.Json;
global using System.Runtime.InteropServices.JavaScript;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Text.RegularExpressions;
global using Azure.OpenAI.Client;
global using Azure.OpenAI.Client.Components;
global using Azure.OpenAI.Client.Extensions;
global using Azure.OpenAI.Client.Interop;
global using Azure.OpenAI.Client.Models;
global using Azure.OpenAI.Client.Services;
global using Azure.OpenAI.Shared;
global using Blazor.Serialization.Extensions;
global using Markdig;
global using Microsoft.AspNetCore.Components;
global using Microsoft.AspNetCore.Components.Web;
global using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
global using Microsoft.Extensions.Localization;
global using Microsoft.Extensions.ObjectPool;
global using Microsoft.JSInterop;
global using MudBlazor;
global using MudBlazor.Services;

global using PromptQuestion = (string Question, bool IsRepeat);

[assembly: System.Runtime.Versioning.SupportedOSPlatform("browser")]