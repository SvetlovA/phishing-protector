using System.Threading;
using System.Threading.Tasks;

namespace PhisihingProtector.Business.Processors;

public interface IProcessor
{
    Task ProcessAsync(CancellationToken cancellationToken = default);
}