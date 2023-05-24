using System.Net.Http;
using AutoFixture;
using Domain.Repositories;
using Domain.UseCases.Abstractions;
using IntegrationTests.Customizations;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace IntegrationTests.Fixures;

public class BaseTestFixture : IClassFixture<SlotMachineWebApplicationFactory>
{
    public BaseTestFixture(SlotMachineWebApplicationFactory factory)
    {
        Factory = factory;
        Autofixture = new();
        Autofixture.Customize(new AutoFixtureCustomizations());

        ConfigurationRepository = Factory.Services.GetRequiredService<IConfigurationRepository>();
        BalanceRepository = Factory.Services.GetRequiredService<IBalanceRepository>();
        HttpClient = Factory.CreateClient();
    }

    protected SlotMachineWebApplicationFactory Factory { get; }

    protected Mock<ISpinUseCase> SpinUseCase => Factory.SpinUseCase;

    protected Fixture Autofixture { get; } 

    protected IConfigurationRepository ConfigurationRepository { get; }

    protected IBalanceRepository BalanceRepository { get; }

    protected HttpClient HttpClient { get; }
}