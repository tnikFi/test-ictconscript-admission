using Application;
using Application.Queries;
using Common.Interfaces;
using Cortex.Mediator;
using Cortex.Mediator.DependencyInjection;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddUserSecrets<IWebMarker>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddCortexMediator(builder.Configuration,
    [typeof(IApplicationMarker)]);

var app = builder.Build();

// Apply database migrations
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await context.Database.MigrateAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Ok("OK"))
    .WithName("HealthCheck")
    .WithOpenApi();

app.MapGet("/entries", async (IMediator mediator) =>
        await mediator.SendAsync<GetLogbookEntriesQuery, IEnumerable<LogbookEntry>>(new GetLogbookEntriesQuery()))
    .WithName("GetLogbookEntries")
    .WithOpenApi();

app.Run();