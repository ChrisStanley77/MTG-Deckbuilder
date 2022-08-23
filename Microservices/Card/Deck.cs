using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Deck
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public List<Card> DeckList { get; set; } = null!;

    public override string ToString()
    {

        string details = Id + " " + Name + " " + DeckList;

        return details;
    }

}