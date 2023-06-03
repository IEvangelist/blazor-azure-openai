// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

var builder = WebApplication.CreateBuilder(args);

using var app = BuildApp(builder);

ConfigureApp(app);

await app.RunAsync();