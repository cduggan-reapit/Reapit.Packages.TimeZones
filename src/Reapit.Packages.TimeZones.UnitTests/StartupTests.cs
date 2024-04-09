using Microsoft.Extensions.DependencyInjection;
using Reapit.Packages.TimeZones.Services.Interfaces;

namespace Reapit.Packages.TimeZones.UnitTests;

public class StartupTests
{
    [Fact]
    public void AddTimeZoneConverterServices_AddsDateTimeConverterService_ToDiContainer()
    {
        var services = new ServiceCollection()
            .AddTimeZoneConverterServices();
        services.Count.Should().Be(1);
        
        var action = () =>
        {
            using var provider = services.BuildServiceProvider();
            return provider.GetRequiredService<IDateTimeConverterService>();
        };
        
        action.Should().NotThrow();
    }
}