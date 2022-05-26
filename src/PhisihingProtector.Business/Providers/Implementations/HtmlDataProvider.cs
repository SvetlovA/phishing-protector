using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var allFilesEnumerable = HtmlFilePatterns.SelectMany(x =>
                Directory.EnumerateFiles(_sourceFilesDirectoryPath, x, SearchOption.AllDirectories));

            foreach (var file in allFilesEnumerable)
            {
                var fileContent = await File.ReadAllTextAsync(file, cancellationToken);
                var htmlDocument = new HtmlDocument();

                htmlDocument.LoadHtml(fileContent);

                yield return new DataEntity<HtmlDocument>
                {
                    Name = file.Replace(_sourceFilesDirectoryPath, string.Empty)
                        .Trim(Path.DirectorySeparatorChar),
                    Content = htmlDocument
                };
            }
        }
    }
}