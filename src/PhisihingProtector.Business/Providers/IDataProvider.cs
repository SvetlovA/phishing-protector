using System.Collections.Generic;
using System.Threading;
using PhisihingProtector.Business.Entities;

namespace PhisihingProtector.Business.Providers;

public interface IDataProvider<TContent>
{
    IAsyncEnumerable<DataEntity<TContent>> GetDataAsync(CancellationToken cancellationToken = default);
}