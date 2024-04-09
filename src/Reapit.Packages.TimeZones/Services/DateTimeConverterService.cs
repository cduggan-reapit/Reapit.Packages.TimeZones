using System.Runtime.Serialization;
using Reapit.Packages.TimeZones.Configuration;
using Reapit.Packages.TimeZones.Services.Interfaces;

namespace Reapit.Packages.TimeZones.Services;

/// <summary>
/// Class defining methods for interacting with DateTimes and TimeZones
/// </summary>
public class DateTimeConverterService : IDateTimeConverterService
{
    /// <inheritdoc/> 
    public DateTime ToUniversalTime(DateTime value, string timezoneCode)
    {
        var timezone = GetTimezone(timezoneCode);
        return new DateTimeOffset(value, timezone.GetUtcOffset(value)).UtcDateTime;
    }

    /// <inheritdoc/>
    public DateTime ToLocalTime(DateTime value, string timezoneCode)
    {
        var timezone = GetTimezone(timezoneCode);
        var utc = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        return TimeZoneInfo.ConvertTimeFromUtc(utc, timezone);
    }

    /// <summary>
    /// Restore TimeZoneInfo from the serialized configuration value for a given key 
    /// </summary>
    /// <param name="timezoneCode">The key of the timezone configuration to restore</param>
    /// <exception cref="KeyNotFoundException">No configuration exists for the provided timezone code.</exception>
    /// <exception cref="ArgumentNullException">The timezone configuration is null.</exception>
    /// <exception cref="SerializationException">The timezone configuration cannot be deserialized into a TimeZoneInfo object.</exception>
    private static TimeZoneInfo GetTimezone(string timezoneCode)
    {
        if(!TimeZoneConfiguration.TimeZones.TryGetValue(timezoneCode, out var serialized))
            throw new KeyNotFoundException(timezoneCode);

        if (string.IsNullOrEmpty(serialized))
            throw new ArgumentNullException(nameof(TimeZoneConfiguration));

        return TimeZoneInfo.FromSerializedString(serialized);
    }
}