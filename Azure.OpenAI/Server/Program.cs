var builder = WebApplication.CreateBuilder(args)
    .AddAzureOpenAI();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
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
app.MapGroup("openai").MapOpenAI();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
