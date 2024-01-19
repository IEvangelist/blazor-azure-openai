// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

public partial class Program
{
    internal static WebApplication BuildApp(WebApplicationBuilder builder)
    {
        builder.Services.AddAzureOpenAI(builder.Configuration);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpClient();
        builder.Services.AddHttpLogging(
            options => options.LoggingFields = HttpLoggingFields.All);

        builder.Services.AddMemoryCache();
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Clear();
            foreach (var converter in JsonSerializationDefaults.Options.Converters)
            {
                options.SerializerOptions.Converters.Add(converter);
            }
        });
        
        return builder.Build();
    }
}
