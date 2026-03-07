using hangfire_test.Components;
using Hangfire;
using Hangfire.MemoryStorage;
using HangfireBlazorDemo.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Hangfire
builder.Services.AddHangfire(config =>
    config.UseMemoryStorage());

builder.Services.AddHangfireServer();

// Job service
builder.Services.AddTransient<MyBackgroundJob>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Optional dashboard
app.UseHangfireDashboard("/hangfire");

app.Run();