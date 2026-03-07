namespace HangfireBlazorDemo.Jobs;

public class MyBackgroundJob
{
    public async Task RunJob()
    {
        Console.WriteLine($"Job started at {DateTime.Now}");

        await Task.Delay(TimeSpan.FromMinutes(10));

        Console.WriteLine($"Job finished after delay at {DateTime.Now}");
    }
}