using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var apiKey = builder.Configuration.GetSection("MyApiKey:Key")?.Value;
builder.Services.Configure<MyApiKeyOptions>(builder.Configuration.GetSection("MyApiKey"));
builder.Services.Configure<List<string>>(builder.Configuration.GetSection("Products"));

builder.Services.ConfigureOptions<MyJwtOptionsSetup>();

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/", (IOptionsMonitor<MyApiKeyOptions> options, IOptions<List<string>> products) =>
{
    string name = options.CurrentValue.Name;
    string key = options.CurrentValue.Key;

    return Results.Ok();
}).RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

class MyJwtOptionsSetup : IPostConfigureOptions<JwtBearerOptions>
{
    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.TokenValidationParameters.ValidateIssuer = true;
        options.TokenValidationParameters.ValidateLifetime = true;
    }
}

class Test
{
    //public Test(IConfiguration configuration)
    //{
    //    var apikey = configuration.GetSection("MyApiKey:Key")?.Value;
    //}

    public Test(IOptions<MyApiKeyOptions> options)
    {
        string name = options.Value.Name;
        string key = options.Value.Key;
    }
}

public sealed class MyApiKeyOptions
{
    public string Name { get; set; } = default!;
    public string Key { get; set; } = default!;
}