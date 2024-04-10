using System.Runtime.Serialization;
using Reapit.Packages.TimeZones.Configuration;
using Reapit.Packages.TimeZones.Services;
using Reapit.Packages.TimeZones.UnitTests.Configuration;

namespace Reapit.Packages.TimeZones.UnitTests.Services;

/// <summary>
/// No need to test the configurations themselves here, only that the service applies the configuration
/// <para>
///     Configurations are tested in:
///     <list type="bullet">
///     <item><see cref="AestConfigurationTests"/></item>
///     <item><see cref="GmtConfigurationTests"/></item>
///     </list>
/// </para>
/// </summary>
public class DateTimeConverterServiceTests : IDisposable
{
    /*
     * ToUniversalTime
     */

    [Fact]
    public void ToUniversalTime_ShouldThrowKeyNotFoundException_WhenKeyNotFound()
    {
        var sut = CreateSut();
        var action = () => sut.ToUniversalTime(DateTime.UnixEpoch, "TST");
        action.Should().Throw<KeyNotFoundException>();
    }
    
    [Fact]
    public void ToUniversalTime_ShouldThrowArgumentNullException_WhenSerializationEmptyNotFound()
    {
        TimeZoneConfiguration.TimeZones.Add("TST", "");
        var sut = CreateSut();
        var action = () => sut.ToUniversalTime(DateTime.UnixEpoch, "TST");
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToUniversalTime_ShouldThrowSerializationException_WhenSerializationMalformed()
    {
        TimeZoneConfiguration.TimeZones.Add("TST", "Check;Check;Check;Break;");
        var sut = CreateSut();
        var action = () => sut.ToUniversalTime(DateTime.UnixEpoch, "TST");
        action.Should().Throw<SerializationException>();
    }

    [Fact]
    public void ToUniversalTime_ShouldReturnTimestampInUtc_FromGivenTimeZone()
    {
        // Local time in AEDT (UTC+1100)
        var local = new DateTime(2024, 2, 9, 23, 15, 12);
        var expected = new DateTime(2024, 2, 9, 12, 15, 12);
        var sut = CreateSut();
        var actual = sut.ToUniversalTime(local, "aest");
        actual.Should().Be(expected);
    }
    
    /*
     * ToLocalTime
     */

    [Fact]
    public void ToLocalTime_ShouldThrowKeyNotFoundException_WhenKeyNotFound()
    {
        var sut = CreateSut();
        var action = () => sut.ToLocalTime(DateTime.UnixEpoch, "TST");
        action.Should().Throw<KeyNotFoundException>();
    }
    
    [Fact]
    public void ToLocalTime_ShouldThrowArgumentNullException_WhenSerializationEmptyNotFound()
    {
        TimeZoneConfiguration.TimeZones.Add("TST", "");
        var sut = CreateSut();
        var action = () => sut.ToLocalTime(DateTime.UnixEpoch, "TST");
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToLocalTime_ShouldThrowSerializationException_WhenSerializationMalformed()
    {
        TimeZoneConfiguration.TimeZones.Add("TST", "Check;Check;Check;Break;");
        var sut = CreateSut();
        var action = () => sut.ToLocalTime(DateTime.UnixEpoch, "TST");
        action.Should().Throw<SerializationException>();
    }

    [Fact]
    public void ToLocalTime_ShouldReturnTimestampInUtc_FromGivenTimeZone()
    {
        // Local time in AEDT (UTC+1100)
        var utc = new DateTime(2024, 2, 9, 12, 15, 12);
        var expected = new DateTime(2024, 2, 9, 23, 15, 12);
        var sut = CreateSut();
        var actual = sut.ToLocalTime(utc, "aest");
        actual.Should().Be(expected);
    }
    
    // Private methods

    private static DateTimeConverterService CreateSut()
        => new();

    /*
     * Implement IDisposable to clear test entries from the TimeZones collection
     */
    
    public void Dispose()
        => TimeZoneConfiguration.TimeZones.Remove("TST");
}