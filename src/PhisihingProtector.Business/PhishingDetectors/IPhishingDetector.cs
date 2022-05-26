namespace PhisihingProtector.Business.PhishingDetectors;

public interface IPhishingDetector<in TContent>
{
    bool ContainsPhishing(TContent fileContent);
}