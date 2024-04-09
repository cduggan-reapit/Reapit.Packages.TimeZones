namespace Reapit.Packages.TimeZones.Services.Interfaces;

/// <summary>
/// Interface describing available methods for interacting with DateTime and TimeZones
/// </summary>
public interface IDateTimeConverterService
{
    /// <summary>
    /// Converts a DateTime representing calendar time in a specified timezone to a DateTime representing instantaneous
    /// time in Coordinated Universal Time (UTC)
    /// </summary>
    /// <param name="value">The time in the local timezone</param>
    /// <param name="timezoneCode">The source timezone</param>
    DateTime ToUniversalTime(DateTime value, string timezoneCode);
    
    /// <summary>
    /// Converts a DateTime representing a time in Coordinated Universal Time (UTC) to a DateTime representing a calendar
    /// time in a specified timezone 
    /// </summary>
    /// <param name="value">The time in UTC</param>
    /// <param name="timezoneCode">The target timezone</param>
    /// <remarks>
    /// Be very careful using the output of this method - it is converted to a fixed timezone, but the server will assume
    /// that it represents the server time zone.  Developers should therefore avoid using ToLocalTime or ToUniversalTime
    /// on the result
    /// </remarks>
    DateTime ToLocalTime(DateTime value, string timezoneCode);
}