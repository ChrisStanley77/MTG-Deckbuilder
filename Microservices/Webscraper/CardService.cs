using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Settings;
using System.Linq;

namespace Service
{
    public class CardService
    {

        private readonly IMongoCollection<Card> _card;
        public CardService(IOptions<CardDatabaseSettings> cardDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                cardDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                cardDatabaseSettings.Value.DatabaseName);

            _card = mongoDatabase.GetCollection<Card>(
                cardDatabaseSettings.Value.CollectionName);
        }

        public async Task<List<Card>> GetAsync() => 
            await _card.Find(_ => true).ToListAsync();

        public async Task<Card?> GetIdAsync(string id) => 
            await _card.Find(i => i.Id.Equals(id)).FirstOrDefaultAsync();

        public async Task<Card?> GetNameAsync(string name) =>
            await _card.Find(i => i.Name.Equals(name)).FirstOrDefaultAsync();

        public async Task<List<Card>> GetTypeAsync(string type) =>
            await _card.Find(i => i.Type.Equals(type)).ToListAsync();

        public async Task<List<Card>> GetSetAsync(string set) =>
            await _card.Find(i => i.Set.Contains(set, StringComparer.OrdinalIgnoreCase)).ToListAsync();

        public async Task<List<Card>> GetCardCostAsync(string ConvertedManaCost) =>
            await _card.Find(i => i.ConvertedManaCost.Equals(ConvertedManaCost)).ToListAsync();

        public async Task<List<Card>> GetCardByColorsAsync(string color) =>
            await _card.Find(i => i.CardColors.Contains(color)).ToListAsync();

        public async Task CreateAsync(Card card) => 
            await _card.InsertOneAsync(card);

        public async Task UpdateAsync(string id, Card newCard) => 
            await _card.ReplaceOneAsync(i => i.Id == id, newCard);

        public async Task RemoveAsync(string name) =>
            await _card.DeleteOneAsync(i => i.Name == name);

        public async Task RemoveAllAsync() =>
            await _card.DeleteManyAsync(Builders<Card>.Filter.Empty);
    }
}