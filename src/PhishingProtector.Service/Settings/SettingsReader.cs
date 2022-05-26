using PhishingProtector.Service.Settings.Config;
using PhishingProtector.Service.Settings.Entities;

namespace PhishingProtector.Service.Settings;

public static class SettingsReader
{
    public static AppSettings ReadSettings(IConfiguration configuration)
    {
        var config = configuration.Get<AppConfigEntity>();

        return new AppSettings
        {
            DataLocationSettings = config.DataLocations.Select(x => new DataLocationSettings
            {
                SourceDirectory = x.SourceDirectory,
                DestinationDirectory = x.DestinationDirectory
            }).ToArray()
        };
    }
}