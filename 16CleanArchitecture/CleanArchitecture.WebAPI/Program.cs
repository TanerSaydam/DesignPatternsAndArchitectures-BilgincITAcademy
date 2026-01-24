using Carter;
using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Options;
using CleanArchitecture.WebAPI;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();

builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddServiceTool();

var conOptions = ServiceTool.ServiceProvider.GetRequiredService<IOptions<ConnectionStringsOptions>>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(conOptions);

builder.Services.AddCarter();

builder.Services.AddRateLimiter(x =>
{
    x.AddFixedWindowLimiter("fixed", cfr =>
    {
        cfr.PermitLimit = 100;
        cfr.QueueLimit = 100;
        cfr.Window = TimeSpan.FromSeconds(1);
        cfr.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});
builder.Services.AddResponseCompression(x => x.EnableForHttps = true);
builder.Services.AddCors();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapDefaultEndpoints();

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .SetPreflightMaxAge(TimeSpan.FromMinutes(10)));

app.UseRateLimiter();
app.UseResponseCompression();

app.MapCarter();

app.Run();