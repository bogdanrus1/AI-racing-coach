using RacingCoach.API.Hubs;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddOpenApi();

builder.Services.AddControllers();

// Add signalR service
builder.Services.AddSignalR();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AIRacingCoachFrontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:1420",
                "http://127.0.0.1:1420"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AIRacingCoachFrontend");

app.MapControllers();

app.MapHub<TelemetryHub>("/hubs/telemetry");

app.MapPost("/test-message", async (IHubContext<TelemetryHub> hub) =>
{
    await hub.Clients.All.SendAsync(
        "ReceiveMessage",
        "Hello from AI Racing Coach backend!"
    );

    return Results.Ok(new { message = "Message sent" });
});

app.Run();
