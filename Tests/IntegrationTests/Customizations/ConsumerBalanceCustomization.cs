using AutoFixture;
using Domain.Entities;
using System;

namespace IntegrationTests.Customizations;

public class ConsumerBalanceCustomization : ICustomization
{
    private static Random Random = new();
    public void Customize(IFixture fixture)
    {
        fixture.Customize<ConsumerBalance>(composer =>
            composer.FromFactory(() => new ConsumerBalance(fixture.Create<string>(), Random.Next(1000, 2000), 1)));
    }
}