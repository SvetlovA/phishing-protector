namespace PhisihingProtector.Business.Entities;

public class DataEntity<TContent>
{
    public string Name { get; set; }
    public TContent Content { get; set; }
}