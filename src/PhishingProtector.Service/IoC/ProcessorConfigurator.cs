using HtmlAgilityPack;
using PhishingProtector.Service.Settings.Entities;
using PhisihingProtector.Business.Entities;
using PhisihingProtector.Business.Factories;
using PhisihingProtector.Business.PhishingDetectors;
using PhisihingProtector.Business.PhishingDetectors.Implementations;

namespace PhishingProtector.Service.IoC;

public static class ProcessorConfigurator
{
    public static void Setup(IServiceCollection serviceCollection, AppSettings settings)
    {
        serviceCollection.AddSingleton<IPhishingDetector<HtmlDocument>, HtmlPhishingDetector>();
        serviceCollection.AddSingleton<IProcessorFactory>(serviceProvider => new HtmlFileProcessorFactory(
            settings.DataLocationSettings.Select(x => new DataLocation
            {
                SourceDirectory = x.SourceDirectory,
                DestinationDirectory = x.DestinationDirectory
            }).ToArray(),
            serviceProvider.GetRequiredService<ILoggerFactory>()));
    }
}