using Steeltoe.Discovery.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConsulDiscoveryClient();

var app = builder.Build();

app.MapGet("/categories", () =>
{
    Console.WriteLine("Hello Categories World!");
    return Results.Ok();
});

app.Run();