// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

global using System.Diagnostics;
global using System.Runtime.CompilerServices;
global using System.Text.Json;
global using System.Text.RegularExpressions;
global using Azure.OpenAI.Client.EndToEndTests.Browser;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Playwright;
global using Xunit;
global using PlaywrightProgram = Microsoft.Playwright.Program;