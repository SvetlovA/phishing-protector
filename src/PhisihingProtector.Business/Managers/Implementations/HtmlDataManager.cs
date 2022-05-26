using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PhisihingProtector.Business.Entities;

namespace PhisihingProtector.Business.Managers.Implementations;

public class HtmlDataManager : IDataManager<HtmlDocument>
{
    private readonly string _destinationDirectory;
    private readonly string _sourceDirectory;

    public HtmlDataManager(string sourceDirectory, string destinationDirectory)
    {
        _destinationDirectory = destinationDirectory;
        _sourceDirectory = sourceDirectory;
    }

    public void RemoveDataFromSource(DataEntity<HtmlDocument> data)
    {
        File.Delete(Path.Combine(_sourceDirectory, data.Name));
    }

    public async Task SaveDataToDestinationAsync(DataEntity<HtmlDocument> data, CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(_destinationDirectory))
        {
            Directory.CreateDirectory(_destinationDirectory);
        }

        await File.WriteAllTextAsync(Path.Combine(_destinationDirectory, data.Name), data.Content.Text, cancellationToken);
    }
}