using Reapit.Packages.TimeZones.Configuration;

namespace Reapit.Packages.TimeZones.UnitTests.Configuration;

public class AestConfigurationTests
{
    /*
     * AEST & AEDT
     * To 2007:
     *  DST From: last Sunday of October @ 0200
     *  DST To: the last Sunday of March @ 0300
     * From 2008:
     *  DST From: first Sunday of October at 0200
     *  DST To: the First Sunday of April at 0300
     */

    [Theory]
    [InlineData("2007-03-25 03:00:00", "2007-10-28 02:59:59")] // Check the original AEST
    [InlineData("2023-04-02 03:00:00", "2023-10-01 02:59:59")] // Check the new AEST
    public void AestConfiguration_ShouldReturnWithBaseOffset_WhenDateNotInDaylightSavingsTime(string start, string end)
    {
        // These should all return UTC+1000
        var expected = TimeSpan.FromHours(10);
        var sut = CreateSut();
        sut.GetUtcOffset(DateTime.Parse(start)).Should().Be(expected, "AEST should have started");
        sut.GetUtcOffset(DateTime.Parse(end)).Should().Be(expected, "AEST should still be in effect");
    }
    
    [Theory]
    [InlineData("2006-10-29 03:00:00", "2007-03-25 01:59:00")] // Check before the change
    [InlineData("2007-10-28 03:00:00", "2008-04-06 01:59:00")] // Check across the change boundary
    [InlineData("2008-10-05 03:00:00", "2009-04-05 01:59:00")] // Check after the change
    public void AestConfiguration_ShouldReturnWithDaylightOffset_WhenDateInDaylightSavingsTime(string start, string end)
    {
        // These should all return UTC+1100
        var expected = TimeSpan.FromHours(11);
        var sut = CreateSut();
        sut.GetUtcOffset(DateTime.Parse(start)).Should().Be(expected, "AEDT should have started");
        sut.GetUtcOffset(DateTime.Parse(end)).Should().Be(expected, "AEDT should still be in effect");
    }

    // Private methods
    
    private static TimeZoneInfo CreateSut(string key = "AEST")
        => TimeZoneInfo.FromSerializedString(TimeZoneConfiguration.TimeZones[key]);
}