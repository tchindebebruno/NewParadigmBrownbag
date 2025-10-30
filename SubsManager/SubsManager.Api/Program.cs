using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using SubsManager.Application.Queries;
using SubsManager.Infrastructure;
using SubsManager.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Services registration
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>());
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserCommand).Assembly);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

// Health check
app.MapGet("/health", () => Results.Ok(new { ok = true }));

// Users
app.MapGet("/users", async (ISender sender) => Results.Ok(await sender.Send(new GetUsersQuery())));
app.MapPost("/users", async (CreateUserCommand cmd, ISender sender) => Results.Ok(await sender.Send(cmd)));
app.MapGet("/users/{id:guid}", async (Guid id, ISender sender) =>
{
    var result = await sender.Send(new GetUserQuery(id));
    return result is null ? Results.NotFound() : Results.Ok(result);
});

// Services & Plans
app.MapGet("/services", async (ISender sender) => Results.Ok(await sender.Send(new GetServicesQuery())));
app.MapPost("/services", async (CreateServiceCommand cmd, ISender sender) => Results.Ok(await sender.Send(cmd)));
app.MapPost("/plans", async (CreatePlanCommand cmd, ISender sender) => Results.Ok(await sender.Send(cmd)));

// Subscriptions
app.MapPost("/subscriptions/subscribe", async (SubscribeCommand cmd, ISender sender) => Results.Ok(await sender.Send(cmd)));
app.MapPost("/subscriptions/cancel", async (CancelSubscriptionCommand cmd, ISender sender) => Results.Ok(await sender.Send(cmd)));
app.MapGet("/users/{userId:guid}/subscriptions/active", async (Guid userId, ISender sender) =>
    Results.Ok(await sender.Send(new GetActiveSubscriptionsByUserQuery(userId))));

await SeedDatabase.RunAsync(app);

app.Run();
