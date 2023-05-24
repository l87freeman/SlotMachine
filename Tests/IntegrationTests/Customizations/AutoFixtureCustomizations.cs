using AutoFixture;

namespace IntegrationTests.Customizations;

public class AutoFixtureCustomizations : CompositeCustomization
{
    public AutoFixtureCustomizations() : base(
        new GridConfigurationCustomization(),
        new ConsumerBalanceCustomization()
    )
    { }
}