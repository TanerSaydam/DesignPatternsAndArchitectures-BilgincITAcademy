using Carter;
using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Options;
using CleanArchitecture.WebAPI;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();

builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddServiceTool();

var conOptions = ServiceTool.ServiceProvider.GetRequiredService<IOptions<ConnectionStringsOptions>>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(conOptions);

builder.Services.AddCarter();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapDefaultEndpoints();

app.MapCarter();

app.Run();