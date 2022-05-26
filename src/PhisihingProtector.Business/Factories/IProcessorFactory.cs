using System.Collections.Generic;
using PhisihingProtector.Business.Processors;

namespace PhisihingProtector.Business.Factories;

public interface IProcessorFactory
{
    IEnumerable<IProcessor> CreateProcessors();
}