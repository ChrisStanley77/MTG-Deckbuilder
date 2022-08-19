using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Card
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string Image { get; set;} = null!;
    public string Name { get; set;} = null!;
    public string? ManaCost { get; set;} = null!;
    public string? ConvertedManaCost { get; set;} = null;
    public string CardColors { get; set;} = null!;
    public string Type { get; set;} = null!;
    public string OracleText { get; set;} = null!;
    public string FlavorText { get; set;} = null!;
    public string Set { get; set;} = null!;
    public string Rarity { get; set;} = null!;
    public string? Power { get; set; }
    public string? Toughness { get; set;}
}