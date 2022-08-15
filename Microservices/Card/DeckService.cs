using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Settings;

namespace Services
{
    public class DeckService
    {
        private readonly IMongoCollection<Deck> _deck;

        public DeckService(IOptions<DeckDatabaseSettings> deckDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                deckDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                deckDatabaseSettings.Value.DatabaseName);

            _deck = mongoDatabase.GetCollection<Deck>(
                deckDatabaseSettings.Value.CollectionName);
        }

        public async Task<List<Deck>> GetAsync() => 
            await _deck.Find(_ => true).ToListAsync();

        public async Task<Deck?> GetIdAsync(string id) => 
            await _deck.Find(i => i.Id == id).FirstOrDefaultAsync();

        public async Task<Deck?> GetNameAsync(string name) =>
            await _deck.Find(i => i.Name == name).FirstOrDefaultAsync();

        public async Task CreateAsync(Deck deck) => 
            await _deck.InsertOneAsync(deck);

        public async Task UpdateAsync(string id, Deck NewDeck) => 
            await _deck.ReplaceOneAsync(i => i.Id == id, NewDeck);

        public async Task RemoveAsync(string name) =>
            await _deck.DeleteOneAsync(i => i.Name == name);

        public async Task RemoveAllAsync() =>
            await _deck.DeleteManyAsync(Builders<Deck>.Filter.Empty);
    }
}