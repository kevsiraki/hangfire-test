using Hangfire.Server;
using Hangfire.Console;
using Microsoft.AspNetCore.SignalR;
using hangfire_test.Hubs;

namespace HangfireBlazorDemo.Jobs;

public class MyBackgroundJob
{
    private readonly IHubContext<ProgressHub> _hub;

    public MyBackgroundJob(IHubContext<ProgressHub> hub)
    {
        _hub = hub;
    }

    public async Task RunJob(PerformContext context)
    {
        await Report(context, 10, "Validating request...");
        await Task.Delay(TimeSpan.FromMinutes(1));

        await Report(context, 30, "Loading data...");
        await Task.Delay(TimeSpan.FromMinutes(2));

        await Report(context, 60, "Processing data...");
        await Task.Delay(TimeSpan.FromMinutes(4));

        await Report(context, 85, "Generating report...");
        await Task.Delay(TimeSpan.FromMinutes(2));

        await Report(context, 100, "Saving results...");
        await Task.Delay(TimeSpan.FromMinutes(1));
    }

    private async Task Report(PerformContext context, int progress, string message)
    {
        // SignalR update
        await _hub.Clients.All.SendAsync("ReceiveProgress", progress, message);

        // Hangfire dashboard log
        context.WriteLine($"{progress}% - {message}");
    }
}