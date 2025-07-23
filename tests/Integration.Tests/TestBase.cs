using Application;
using Common.Interfaces;
using Cortex.Mediator;
using Cortex.Mediator.DependencyInjection;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integration.Tests;

public class TestBase : IAsyncLifetime
{
    private IServiceProvider? _serviceProvider;
    private IServiceProvider ServiceProvider => _serviceProvider ?? throw new InvalidOperationException();
    protected IMediator Mediator => ServiceProvider.GetRequiredService<IMediator>();
    protected IApplicationDbContext Context => ServiceProvider.GetRequiredService<IApplicationDbContext>();

    public Task InitializeAsync()
    {
        var services = new ServiceCollection();
        var config = new ConfigurationBuilder()
            .Build();
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();
        services.AddCortexMediator(
            config,
            [typeof(IApplicationMarker)]
        );
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(opts =>
        {
            opts.UseInMemoryDatabase(Guid.NewGuid().ToString());
            opts.EnableSensitiveDataLogging();
        });
        _serviceProvider = services.BuildServiceProvider();
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        // Clean up data
        var entries = await Context.LogbookEntries.ToListAsync();
        Context.LogbookEntries.RemoveRange(entries);
        await Context.SaveChangesAsync();
        
        _serviceProvider = null!;
    }

    /// <summary>
    /// Helper method to send a <see cref="IValueCommand{TResult}"/> with the Mediator
    /// and automatically validate the input if a validator exists.
    /// </summary>
    /// <param name="command"></param>
    /// <typeparam name="TValueCommand"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    protected async Task<TResult> SendAsync<TValueCommand, TResult>(TValueCommand command)
        where TValueCommand : IValueCommand<TResult>
    {
        var validator = ServiceProvider.GetService<IValidator<TValueCommand>>();
        if (validator != null)
            await validator.ValidateAndThrowAsync(command);
        return await Mediator.SendAsync<TValueCommand, TResult>(command);
    }
}