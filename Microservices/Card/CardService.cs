using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Settings;

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
            await _card.Find(i => i.Id == id).FirstOrDefaultAsync();

        public async Task<Card?> GetNameAsync(string name) =>
            await _card.Find(i => i.Name == name).FirstOrDefaultAsync();

        public async Task<List<Card>> GetTypeAsync(string type) =>
            await _card.Find(i => i.Type == type).ToListAsync();

        public async Task<List<Card>> GetSetAsync(string set) =>
            await _card.Find(i => i.Set == set).ToListAsync();

        public async Task<List<Card>> GetCardCostAsync(int ConvertedManaCost) =>
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