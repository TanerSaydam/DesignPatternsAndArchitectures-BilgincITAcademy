using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddRateLimiter(x =>
{
    x.AddFixedWindowLimiter("fixed", cfr =>
    {
        cfr.Window = TimeSpan.FromMinutes(1);
        cfr.PermitLimit = 2;
        cfr.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        cfr.QueueLimit = 2;
    });

    //x.AddSlidingWindowLimiter()

    //x.AddConcurrencyLimiter()

    //x.AddTokenBucketLimiter()
});

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseRateLimiter();

app.MapGet("/", () => "Hello World!").RequireRateLimiting("fixed");

app.Run();