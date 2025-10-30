using AgentBased.Api.Endpoints;
using AgentBased.Application;
using AgentBased.Infrastructure;
using AgentBased.Infrastructure.Constants;
using OpenTelemetry;
using OpenTelemetry.Trace;
using Scriban;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
var builder = WebApplication.CreateBuilder(args);

Sdk.CreateTracerProviderBuilder()
    .AddSource(ConfigurationConstants.AgentTelemetrySourceName)
    .AddConsoleExporter()
    .Build();

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOpenApi();
builder.Services.AddLogging(builder =>
{
    builder.ClearProviders();
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Debug);
});
builder.Services.AddSingleton<IPromptCollection<Template>, PromptCollection>();
builder.Services.AddSubsManagerTools(builder.Configuration);
builder.Services.AddMyAgent(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapUsersEndpoints();

app.Run();
