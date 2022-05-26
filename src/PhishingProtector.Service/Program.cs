using PhishingProtector.Service.IoC;
using PhishingProtector.Service.Settings;

var builder = Host.CreateDefaultBuilder();
builder.ConfigureServices((context, serviceCollection) =>
{
    var settings = SettingsReader.ReadSettings(context.Configuration);

    HostedServicesConfigurator.Setup(serviceCollection, settings);
});

var app = builder.Build();

await app.RunAsync();
