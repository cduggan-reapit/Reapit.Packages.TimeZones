namespace Reapit.Packages.TimeZones.Configuration;

/// <summary>
/// Methods and properties defining time zone configurations
/// </summary>
/// <remarks>This should not be a long-term solution; configurations should be read from an external source as we scale.</remarks>
internal static class TimeZoneConfiguration
{
    /// <summary>
    /// Dictionary of time zone configurations for standard time zone identifiers (e.g. GMT, AEST)
    /// </summary>
    public static readonly Dictionary<string, string> TimeZones = new(StringComparer.OrdinalIgnoreCase)
    {
        { "GMT", Gmt },
        { "AEST", Aest }
    };
    
    /// <summary>
    /// Timezone configuration for Greenwich Mean Time and British Summer Time as observed in the United Kingdom
    /// </summary>
    private const string Gmt = "GMT Standard Time;0;(UTC+00:00) Dublin, Edinburgh, Lisbon, London;GMT Standard Time;GMT Summer Time;[01:01:0001;12:31:9999;60;[0;01:00:00;3;5;0;];[0;02:00:00;10;5;0;];];";

    /// <summary>
    /// Timezone configuration for Australian Eastern Standard Time and Australian Eastern Daylight Time as observed in New South Wales, Tasmania, Victoria, and Australian Capital Territory
    /// </summary>
    private const string Aest = "AUS Eastern Standard Time;600;(UTC+10:00) Canberra, Melbourne, Sydney;AUS Eastern Standard Time;AUS Eastern Summer Time;[01:01:0001;12:31:2007;60;[0;02:00:00;10;5;0;];[0;03:00:00;3;5;0;];][01:01:2008;12:31:9999;60;[0;02:00:00;10;1;0;];[0;03:00:00;4;1;0;];];";
}