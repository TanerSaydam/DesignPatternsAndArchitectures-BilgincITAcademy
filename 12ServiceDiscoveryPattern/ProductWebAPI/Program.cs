using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConsulDiscoveryClient();
builder.Services.AddHttpClient();


var app = builder.Build();

app.MapGet("/", async (HttpClient httpClient, IDiscoveryClient client) =>
{
    var res = await client.GetInstancesAsync("CategoryWebAPI", default);
    var uri = res.First().Uri;
    var message = await httpClient.GetAsync(uri + "categories");
    return Results.Ok();
});

app.Run();
