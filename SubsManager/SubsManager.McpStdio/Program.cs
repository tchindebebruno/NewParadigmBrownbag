using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SubsManager.Application.UseCases;
using SubsManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Logging.AddConsole(o => { o.LogToStandardErrorThreshold = LogLevel.Trace; });
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>());
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserCommand).Assembly);
builder.Services.AddInfrastructure(builder.Configuration);

builder
    .Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithTools<WeatherTools>()
    .WithToolsFromAssembly();

var app = builder.Build();

app.MapMcp();

app.Run();
