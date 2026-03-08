using hangfire_test.Components;
using Hangfire;
using Hangfire.SqlServer;
using HangfireBlazorDemo.Jobs;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(
        builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();

builder.Services.AddTransient<MyBackgroundJob>();

var app = builder.Build();
app.MapStaticAssets();
app.UseAntiforgery();

app.UseHangfireDashboard("/hangfire");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();