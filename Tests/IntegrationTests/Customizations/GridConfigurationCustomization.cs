using AutoFixture;
using Domain.Entities;
using System;

namespace IntegrationTests.Customizations;

public class GridConfigurationCustomization : ICustomization
{
    private static Random Random = new();
    public void Customize(IFixture fixture)
    {
        fixture.Customize<GridConfiguration>(composer =>
            composer.With(x => x.Id, () => fixture.Create<string>())
                .With(x => x.Width, () => Random.Next(3, 10))
                .With(x => x.Height, () => Random.Next(3, 10))
            );
    }
}