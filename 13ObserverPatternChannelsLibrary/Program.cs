using _13ObserverPatternChannelsLibrary;

var builder = WebApplication.CreateBuilder(args);

//WeaterSystem weaterSystem = new();
//WeaterLog weaterLog = new();
//WeaterUI weaterUI = new();
//weaterSystem.Subscribe(weaterUI);
//weaterSystem.Subscribe(weaterLog);
//weaterSystem.SetWeater(25);

builder.Services.AddSingleton<WeaterSystem>();
builder.Services.AddSingleton<WeaterLog>();
builder.Services.AddSingleton<WeaterUI>();
builder.Services.AddHostedService<SubscribeBackgroundService>();
builder.Services.AddHostedService<QueueBackgroundService>();

builder.Services.AddSingleton<MyQueue>();

var app = builder.Build();

app.MapGet("/subscribe", (WeaterSystem weaterSystem, WeaterUI weaterUI, WeaterLog weaterLog) =>
{
    weaterSystem.Subscribe(weaterUI);
    weaterSystem.Subscribe(weaterLog);
    return Results.Ok();
});

app.MapGet("/change-weater", (int weater, WeaterSystem weaterSystem) =>
{
    weaterSystem.SetWeater(weater);
    return Results.Ok();
});

app.MapGet("/send-queue", async (MyQueue myQueue) =>
{
    var info = new MyInfo("Taner Saydam");
    await myQueue._channel.Writer.WriteAsync(info);
    return Results.Ok();
});

app.Run();

public class WeaterSystem : IObservable<int>
{
    private List<IObserver<int>> _observers = new();
    private int _weater;
    public IDisposable Subscribe(IObserver<int> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return new Unsubscriber(_observers, observer);
    }

    public void SetWeater(int weater)
    {
        _weater = weater;
        foreach (var observer in _observers)
        {
            observer.OnNext(weater);
        }
    }

    public class Unsubscriber : IDisposable
    {
        public List<IObserver<int>> _observers;
        public IObserver<int> _observer;

        public Unsubscriber(List<IObserver<int>> observers, IObserver<int> observer)
        {
            _observers = observers;
            _observer = observer;
        }
        public void Dispose()
        {
            if (_observer is not null && _observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}

public class WeaterLog : IObserver<int>
{
    public void OnCompleted()
    {
        Console.WriteLine("Weater sistemi kapandý");
    }

    public void OnError(Exception error)
    {
        Console.WriteLine("Error: {0}", error.Message);
    }

    public void OnNext(int value)
    {
        Console.WriteLine("Weater LOG is {0}", value);
    }
}

public class WeaterUI : IObserver<int>
{
    public void OnCompleted()
    {
        Console.WriteLine("Weater sistemi kapandý");
    }

    public void OnError(Exception error)
    {
        Console.WriteLine("Error: {0}", error.Message);
    }

    public void OnNext(int value)
    {
        Console.WriteLine("Weater UI is {0}", value);
    }
}