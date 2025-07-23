using Application;
using Common.Interfaces;
using Cortex.Mediator;
using Cortex.Mediator.DependencyInjection;
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
}