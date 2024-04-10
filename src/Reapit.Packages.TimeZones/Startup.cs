using Microsoft.Extensions.DependencyInjection;
using Reapit.Packages.TimeZones.Services;
using Reapit.Packages.TimeZones.Services.Interfaces;

namespace Reapit.Packages.TimeZones;

/// <summary>
/// DI container setup methods for services in the Reapit.Packages.TimeZones project
/// </summary>
public static class Startup
{
    /// <summary>Adds services to the specified IServiceCollection.</summary>
    /// <param name="services">The IServiceCollection to add to</param>
    /// <returns>A reference to the IServiceCollection after the operations have been completed</returns>
    public static IServiceCollection AddTimeZoneConverterServices(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeConverterService, DateTimeConverterService>();
        return services;
    }
}