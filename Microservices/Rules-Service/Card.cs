
public class Card
{
    public string Id { get; set; } = null!;
    public string Image { get; set;} = null!;
    public string Name { get; set;} = null!;
    public int ConvertedManaCost { get; set;} = 0!;
    public List<string> CardColors { get; set;} = null!;
    public string Type { get; set;} = null!;
    public string Text { get; set;} = null!;
    public string Set { get; set;} = null!;
    public string? Stats { get; set; }
}