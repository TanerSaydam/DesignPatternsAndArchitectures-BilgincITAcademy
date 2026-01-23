namespace _12OutboxPattern;

public sealed class MyBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.CompletedTask;
        //dbye bağlanıp
        //outbox listemde is completed = false olanları alırım
        //for döngüsüyle işlemi
    }
}