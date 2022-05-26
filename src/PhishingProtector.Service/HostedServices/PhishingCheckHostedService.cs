using PhisihingProtector.Business.Factories;

namespace PhishingProtector.Service.HostedServices;

public class PhishingCheckHostedService : BackgroundService
{
    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(3);

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

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.WhenAll(_processorFactory.CreateProcessors().Select(x => x.ProcessAsync(stoppingToken)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while processing. Retry...");
                await Task.Delay(RetryDelay, stoppingToken);
            }
        }

        _logger.LogInformation("Stop processing");
    }
}