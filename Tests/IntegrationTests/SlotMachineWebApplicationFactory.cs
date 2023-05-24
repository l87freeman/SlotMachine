using System.Collections.Generic;
using Domain.UseCases;
using Domain.UseCases.Abstractions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mongo2Go;
using Moq;

namespace IntegrationTests;

public class SlotMachineWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly MongoDbRunner _mongoRunner;

    public Mock<ISpinUseCase> SpinUseCase { get; } = new();

    public SlotMachineWebApplicationFactory()
    {
        _mongoRunner = MongoDbRunner.StartForDebugging();
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(c => c.AddInMemoryCollection(new List<KeyValuePair<string, string>>
        {
            new("MongoDb:ConnectionString", _mongoRunner.ConnectionString),
            new("MongoDb:DatabaseName", "SlotMachineTests"),
            new("MongoDb:BalanceCollectionName", "balances"),
            new("MongoDb:GridConfigurationCollectionName", "configuration"),
        }));

        builder.ConfigureServices((b, s) =>
        {
            s.Remove(new ServiceDescriptor(typeof(ISpinUseCase), typeof(SpinUseCase), ServiceLifetime.Scoped));
            s.AddSingleton<ISpinUseCase>(SpinUseCase.Object);
        });
        return base.CreateHost(builder);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _mongoRunner.Dispose();
    }
}