using HealthCheck.TestingWebApp;
using HealthCheck.TestingWebApp.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(sqlConnectionString);
});

builder.Services.AddHealthChecks()
    .AddCheck<TestingHealthCheck>("Testing", HealthStatus.Healthy);

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

var app = builder.Build();

app.MapGet("/SetHealthy/{isHealthy:bool}", (bool isHealthy) =>
{
    TestingHealthCheck.IsHealthy = isHealthy;
    return TestingHealthCheck.IsHealthy ? "Hello Healthy World!" : "Hello Unhealthy World!";
});
app.MapHealthChecks("/healthz");

app.MapGet("/", () => "Hello World!");

app.Run();
