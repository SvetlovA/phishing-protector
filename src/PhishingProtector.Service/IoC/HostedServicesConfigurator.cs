using PhishingProtector.Service.HostedServices;
using PhishingProtector.Service.Settings.Entities;

namespace PhishingProtector.Service.IoC;

public static class HostedServicesConfigurator
{
    public static void Setup(IServiceCollection serviceCollection, AppSettings settings)
    {
        ProcessorConfigurator.Setup(serviceCollection, settings);

        serviceCollection.AddHostedService<PhishingCheckHostedService>();
    }
}