using PhisihingProtector.Business.Factories;

namespace PhishingProtector.Service.HostedServices;

public class PhishingCheckHostedService : BackgroundService
{
    private readonly IProcessorFactory _processorFactory;
    private readonly ILogger<PhishingCheckHostedService> _logger;

    public PhishingCheckHostedService(IProcessorFactory processorFactory, ILogger<PhishingCheckHostedService> logger)
    {
        _processorFactory = processorFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Start processing");

        await Task.WhenAll(_processorFactory.CreateProcessors().Select(x => x.ProcessAsync(stoppingToken)));

        _logger.LogInformation("Stop processing");
    }
}