using Application;
using Application.Commands.AddLogbookEntry;
using Application.Queries;
using Application.Queries.GetLogbookEntry;
using Common.Interfaces;
using Cortex.Mediator;
using Cortex.Mediator.DependencyInjection;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Web;
using Web.ExceptionHandlers;

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

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Apply database migrations
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await context.Database.MigrateAsync();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok("OK"))
    .WithName("HealthCheck")
    .WithOpenApi();

app.MapGet("/entries", async (IMediator mediator) =>
        await mediator.SendAsync<GetLogbookEntriesQuery, IEnumerable<LogbookEntry>>(new GetLogbookEntriesQuery()))
    .WithName("GetLogbookEntries")
    .WithOpenApi();

app.MapGet("/entries/{id}",
    async (int id, IMediator mediator) =>
        await mediator.SendAsync<GetLogbookEntryQuery, LogbookEntry>(new GetLogbookEntryQuery(id)))
    .WithName("GetLogbookEntry")
    .WithOpenApi();

app.MapPost("/entries",
        async (IMediator mediator, AddLogbookEntryCommand command) =>
            await mediator.SendAsync<AddLogbookEntryCommand, LogbookEntry>(command))
    .WithName("AddLogbookEntry")
    .AddFluentValidationAutoValidation()
    .WithOpenApi();

app.Run();