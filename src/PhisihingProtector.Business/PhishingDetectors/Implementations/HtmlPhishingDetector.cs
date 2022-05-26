using System.Linq;
using HtmlAgilityPack;

namespace PhisihingProtector.Business.PhishingDetectors.Implementations;

public class HtmlPhishingDetector : IPhishingDetector<HtmlDocument>
{
    private const string LinkTagName = "a";
    private const string HrefAttributeName = "href";

    public bool ContainsPhishing(HtmlDocument fileContent)
    {
        var linkNodes = fileContent.DocumentNode.Descendants(LinkTagName);

        return linkNodes.Any(linkNode => linkNode.GetAttributeValue(HrefAttributeName, null) != linkNode.InnerHtml);
    }
}