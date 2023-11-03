using HealthCheck.TestingWebApp;

using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck<TestingHealthCheck>("Testing", HealthStatus.Healthy);

var app = builder.Build();

app.MapGet("/SetHealthy/{isHealthy:bool}", (bool isHealthy) =>
{
    TestingHealthCheck.IsHealthy = isHealthy;
    return TestingHealthCheck.IsHealthy ? "Hello Healthy World!" : "Hello Unhealthy World!";
});
app.MapHealthChecks("/healthz");

app.MapGet("/", () => "Hello World!");

app.Run();
