# Reapit.Packages.TimeZones

Package containing methods and services for use when converting DateTime values between time-zones

## Usage

- Register the time-zone services in Startup:
```csharp
using Reapit.Packages.TimeZones.Startup;
...
builder.Services.AddTimeZoneConverterServices();
```

- Inject the IDateTimeConverterService wherever you need to convert between a local time-zone and UTC
```csharp
public class MyClass 
{
    private readonly IDateTimeConverterService _dateTimeConverterService;

    public MyClass(IDateTimeConverterService dateTimeConverterService)
        => _dateTimeConverterService = dateTimeConverterService
        
    public DateTime GetNowInAustralianTime()
        => _dateTimeConverterService.ToLocalTime(DateTime.UtcNow, "AEST");
    
    public DateTime GetAustralianTimeInUtc(DateTime australianTime)
        => _dateTimeConverterService.ToUniversalTime(australianTime, "AEST");
}
```

## Supported time-zones

This package currently supports the following time-zones:

| Code | Name                            | Base Offset | DST Offset | Notes                                                    |
|---------------|----------------------------------|----------------------------|------------|----------------------------------------------------------|
| GMT           | Greenwich Mean Time              | UTC+0000                   | UTC+0100   | Observes BST between March and October (UK)              |
| AEST          | Australian Eastern Standard Time | UTC+1000                   | UTC+1100   | Observes AEDT during the summer months (ACT/NSW/TAS/VIC) |
