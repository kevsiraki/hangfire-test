using Hangfire;
using Hangfire.Console;
using Hangfire.SqlServer;
using hangfire_test.Components;
using hangfire_test.Hubs;
using HangfireBlazorDemo.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSignalR();

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(
        builder.Configuration.GetConnectionString("HangfireConnection"));

    config.UseConsole();
});

builder.Services.AddHangfireServer();

builder.Services.AddTransient<MyBackgroundJob>();

var app = builder.Build();

app.MapStaticAssets();
app.UseAntiforgery();

app.UseHangfireDashboard("/hangfire");

app.MapHub<ProgressHub>("/progresshub");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();