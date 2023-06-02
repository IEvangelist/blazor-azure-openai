// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

public static partial class Program
{
    internal static void ConfigureApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();
        app.UseRouting();
        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.MapAzureOpenAiApi();
    }
}
