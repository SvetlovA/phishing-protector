using System.Threading;
using System.Threading.Tasks;
using PhisihingProtector.Business.Entities;

namespace PhisihingProtector.Business.Managers;

public interface IDataManager<TContent>
{
    void RemoveDataFromSource(DataEntity<TContent> data);
    Task SaveDataToDestinationAsync(DataEntity<TContent> data, CancellationToken cancellationToken = default);
}