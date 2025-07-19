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
            opts.UseInMemoryDatabase("TestDb");
            opts.EnableSensitiveDataLogging();
        });
        _serviceProvider = services.BuildServiceProvider();
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        _serviceProvider = null!;
        return Task.CompletedTask;
    }
}