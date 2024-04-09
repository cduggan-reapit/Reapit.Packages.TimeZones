using FluentAssertions;
using Reapit.Packages.TimeZones.Configuration;

namespace Reapit.Packages.TimeZones.UnitTests.Configuration;

public class GmtConfigurationTests
{
    /*
     * GMT & BST
     * DST From: the last Sunday of March @ 0100
     * DST To: last Sunday of October @ 0200 
     */

    [Theory]
    [InlineData("2019-10-27 02:00:00", "2020-03-29 00:59:59")]
    [InlineData("2020-10-25 02:00:00", "2021-03-28 00:59:59")]
    [InlineData("2021-10-31 02:00:00", "2022-03-27 00:59:59")]
    [InlineData("2022-10-30 02:00:00", "2023-03-26 00:59:59")]
    [InlineData("2023-10-29 02:00:00", "2024-03-31 00:59:59")]
    public void GmtConfiguration_ShouldReturnWithBaseOffset_WhenDateNotInDaylightSavingsTime(string gmtStart, string gmtEnd)
    {
        // These should all return UTC+0000
        var sut = CreateSut();
        sut.GetUtcOffset(DateTime.Parse(gmtStart)).Should().Be(TimeSpan.Zero);
        sut.GetUtcOffset(DateTime.Parse(gmtEnd)).Should().Be(TimeSpan.Zero);
    }
    
    [Theory]
    [InlineData("2019-03-31 02:00:00", "2019-10-27 00:59:00")]
    [InlineData("2020-03-29 02:00:00", "2020-10-25 00:59:00")]
    [InlineData("2021-03-28 02:00:00", "2021-10-31 00:59:00")]
    [InlineData("2022-03-27 02:00:00", "2022-10-30 00:59:00")]
    [InlineData("2023-03-26 02:00:00", "2023-10-29 00:59:00")]
    public void GmtConfiguration_ShouldReturnWithDaylightOffset_WhenDateInDaylightSavingsTime(string bstStart, string bstEnd)
    {
        // These should all return UTC+0100
        var expected = TimeSpan.FromHours(1);
        var sut = CreateSut();
        sut.GetUtcOffset(DateTime.Parse(bstStart)).Should().Be(expected);
        sut.GetUtcOffset(DateTime.Parse(bstEnd)).Should().Be(expected);
    }

    private TimeZoneInfo CreateSut(string key = "GMT")
        => TimeZoneInfo.FromSerializedString(TimeZoneConfiguration.TimeZones[key]);
}