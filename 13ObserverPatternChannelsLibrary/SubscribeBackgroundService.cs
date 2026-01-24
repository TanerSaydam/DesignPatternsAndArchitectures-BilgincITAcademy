
namespace _13ObserverPatternChannelsLibrary;

public sealed class SubscribeBackgroundService(
    WeaterSystem weaterSystem,
    WeaterUI weaterUI,
    WeaterLog weaterLog) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        weaterSystem.Subscribe(weaterUI);
        weaterSystem.Subscribe(weaterLog);

        return Task.CompletedTask;
    }
}

public sealed class QueueBackgroundService(
    MyQueue myQueue) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var queue in myQueue._channel.Reader.ReadAllAsync())
        {
            Console.WriteLine("Name is {0}", queue.Name);
            await Task.Delay(500);
        }
    }
}
