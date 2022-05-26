using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using HtmlAgilityPack;
using PhisihingProtector.Business.Entities;

namespace PhisihingProtector.Business.Providers.Implementations;

public class HtmlDataProvider : IDataProvider<HtmlDocument>
{
    private static readonly string[] HtmlFilePatterns = { "*.htm", "*.html" };

    private readonly string _sourceFilesDirectoryPath;

    public HtmlDataProvider(string sourceFilesDirectoryPath)
    {
        _sourceFilesDirectoryPath = sourceFilesDirectoryPath;
    }

    public async IAsyncEnumerable<DataEntity<HtmlDocument>> GetDataAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(_sourceFilesDirectoryPath))
        {
            throw new Exception($"{_sourceFilesDirectoryPath} isn't exists or not valid path");
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            foreach (var htmlFilePattern in HtmlFilePatterns)
            {
                foreach (var file in Directory.EnumerateFiles(_sourceFilesDirectoryPath, htmlFilePattern, SearchOption.AllDirectories))
                {
                    var fileContent = await File.ReadAllTextAsync(file, cancellationToken);
                    var htmlDocument = new HtmlDocument();

                    htmlDocument.LoadHtml(fileContent);

                    yield return new DataEntity<HtmlDocument>
                    {
                        Name = Path.GetFileName(file),
                        Content = htmlDocument
                    };
                }
            }
        }
    }
}