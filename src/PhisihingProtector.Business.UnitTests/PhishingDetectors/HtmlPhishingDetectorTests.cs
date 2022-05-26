using HtmlAgilityPack;
using NUnit.Framework;
using PhisihingProtector.Business.PhishingDetectors.Implementations;

namespace PhisihingProtector.Business_UnitTests.PhishingDetectors
{
    [TestFixture]
    public class HtmlPhishingDetectorTests
    {
        private static readonly string HtmlWithoutPhishing = @"
<!DOCTYPE html>
<html>

<head>
    <!-- HTML Codes by Quackit.com -->
    <title>
    </title>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
</head>

<body>
    <a href=""https://www.google.com/"">https://www.google.com/</a>
    <h1>Header</h1>
    <a href=""https://www.google.com/"">https://www.google.com/</a>
    <p>Paragraph</p>
    <a href=""https://www.google.com/"">https://www.google.com/</a>
</body>

</html>
";

        private static readonly string HtmlWithPhishing = @"
<!DOCTYPE html>
<html>

<head>
    <!-- HTML Codes by Quackit.com -->
    <title>
    </title>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
</head>

<body>
    <a href=""https://www.google.com/"">https://www.google.com/</a>
    <h1>Header</h1>
    <a href=""https://www.paypal.com/il/home."">Phishing</a>
    <p>Paragraph</p>
    <a href=""https://www.google.com/"">https://www.google.com/</a>
</body>

</html>
";

        [Test]
        [TestCaseSource(nameof(TestContainsPhishingData))]
        public bool TestContainsPhishing(string inputHtml)
        {
            var phishingDetector = new HtmlPhishingDetector();

            var document = new HtmlDocument();
            document.LoadHtml(inputHtml);

            return phishingDetector.ContainsPhishing(document);
        }

        private static IEnumerable<TestCaseData> TestContainsPhishingData()
        {
            yield return new TestCaseData(HtmlWithPhishing).Returns(true).SetName("TestHtmlWithPhishing");
            yield return new TestCaseData(HtmlWithoutPhishing).Returns(false).SetName("TestHtmlWithoutPhishing");
        }
    }
}
