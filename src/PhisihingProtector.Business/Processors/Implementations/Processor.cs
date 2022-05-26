using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PhisihingProtector.Business.Managers;
using PhisihingProtector.Business.PhishingDetectors;
using PhisihingProtector.Business.Providers;

namespace PhisihingProtector.Business.Processors.Implementations;

public class Processor<TContent> : IProcessor
{
    private readonly IDataProvider<TContent> _dataProvider;
    private readonly IPhishingDetector<TContent> _dataPhisihingDetector;
    private readonly IDataManager<TContent> _dataManager;
    private readonly ILogger<Processor<TContent>> _logger;

    public Processor(
        IDataProvider<TContent> dataProvider,
        IPhishingDetector<TContent> dataPhisihingDetector,
        IDataManager<TContent> dataManager,
        ILogger<Processor<TContent>> logger)
    {
        _dataProvider = dataProvider;
        _dataPhisihingDetector = dataPhisihingDetector;
        _dataManager = dataManager;
        _logger = logger;
    }

    public Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        return Task.Run(async () =>
        {
            await foreach (var data in _dataProvider.GetDataAsync(cancellationToken))
            {
                _logger.LogInformation($"Start processing {data.Name}");

                if (!_dataPhisihingDetector.ContainsPhishing(data.Content))
                {
                    _logger.LogInformation($"Phishing in {data.Name} wasn't detected");
                    _logger.LogInformation($"Start saving {data.Name}");
                    await _dataManager.SaveDataToDestinationAsync(data, cancellationToken);
                    _logger.LogInformation($"{data.Name} was successfully saved");
                }
                else
                {
                    _logger.LogWarning($"Phishing was detected in {data.Name}. File will be delete");
                }

                _logger.LogInformation($"Start removing {data.Name}");
                _dataManager.RemoveDataFromSource(data);
                _logger.LogInformation($"{data.Name} was successfully removed");

                _logger.LogInformation($"{data.Name} was processed");
            }
        }, cancellationToken);
    }
}