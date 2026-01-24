using FluentEmail.Core;
using Polly;
using Polly.Registry;
using Polly.Retry;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddResiliencePipeline("http", configure =>
{
    configure.AddRetry(new RetryStrategyOptions()
    {
        MaxRetryAttempts = 3,
        Delay = TimeSpan.FromSeconds(3),
        ShouldHandle = new PredicateBuilder().Handle<HttpRequestException>()
    });

    configure.AddRetry(new RetryStrategyOptions()
    {
        MaxRetryAttempts = 5,
        Delay = TimeSpan.FromSeconds(10),
        ShouldHandle = new PredicateBuilder().Handle<SmtpException>()
    }).AddTimeout(TimeSpan.FromSeconds(40));
});

builder.Services.AddFluentEmail("info@taner.com").AddSmtpSender("localhost", 25);

var app = builder.Build();

app.MapGet("/todos", async (HttpClient httpClient, ResiliencePipelineProvider<string> pipelineProvider) =>
{
    //await httpClient.GetFromJsonAsync<List<Todo>>("https://jsonplaceholder.typico1de.com/todos");
    var pipeline = pipelineProvider.GetPipeline("http");
    var todos = await pipeline.ExecuteAsync(async st =>
    {
        return await httpClient.GetFromJsonAsync<List<Todo>>("https://jsonplaceholder.typico1de.com/todos");
    });
    return Results.Ok(todos);
});

app.MapGet("/send-email", async (IFluentEmail fluentEmail, ResiliencePipelineProvider<string> pipelineProvider) =>
{
    var pipeline = pipelineProvider.GetPipeline("http");
    var todos = await pipeline.ExecuteAsync(async st =>
    {
        return await fluentEmail.To("tanersaydam@gmail.com").Subject("Hello").Body("Hello world").SendAsync();
    });
    return Results.Ok();
});



app.Run();

class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
}