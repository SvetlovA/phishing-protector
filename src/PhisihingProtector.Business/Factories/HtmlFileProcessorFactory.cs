using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using PhisihingProtector.Business.Entities;
using PhisihingProtector.Business.Managers.Implementations;
using PhisihingProtector.Business.PhishingDetectors.Implementations;
using PhisihingProtector.Business.Processors;
using PhisihingProtector.Business.Processors.Implementations;
using PhisihingProtector.Business.Providers.Implementations;

namespace PhisihingProtector.Business.Factories;

public class HtmlFileProcessorFactory : IProcessorFactory
{
    private readonly DataLocation[] _dataLocations;
    private readonly ILoggerFactory _loggerFactory;

    public HtmlFileProcessorFactory(
        DataLocation[] dataLocations,
        ILoggerFactory loggerFactory)
    {
        _dataLocations = dataLocations;
        _loggerFactory = loggerFactory;
    }

    public IEnumerable<IProcessor> CreateProcessors()
    {
        return _dataLocations.Select(x => new Processor<HtmlDocument>(
            new HtmlDataProvider(x.SourceDirectory),
            new HtmlPhishingDetector(),
            new HtmlDataManager(
                x.SourceDirectory,
                x.DestinationDirectory),
            _loggerFactory.CreateLogger<Processor<HtmlDocument>>()));
    }
}