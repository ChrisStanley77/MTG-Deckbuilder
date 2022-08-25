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

        public async Task<List<Card>> GetNameAsync(string name)
        {
            string nameLower = name.ToLower();
            var filter = Builders<Card>.Filter.Where(c => c.Name.ToLower().Contains(nameLower));
            return await _card.Find(filter).ToListAsync();
        }

        public async Task<List<Card>> GetTypeAsync(string type) 
        {
            var typeLower = type.ToLower();
            var filter = Builders<Card>.Filter.Where(c => c.Type.ToLower().Contains(typeLower));
            return await _card.Find(filter).ToListAsync();
        }
            

        public async Task<List<Card>> GetSetAsync(string set)
        {
            var setLower = set.ToLower();
            var filter = Builders<Card>.Filter.Where(c => c.Set.ToLower().Contains(setLower));
            return await _card.Find(filter).ToListAsync();
        }

        public async Task<List<Card>> GetCardCostAsync(string ConvertedManaCost)
        {
            var cmc = ConvertedManaCost.ToLower();
            var filter = Builders<Card>.Filter.Where(c => c.ConvertedManaCost.ToLower().Contains(cmc));
            return await _card.Find(filter).ToListAsync();
        }

        public async Task<List<Card>> GetCardByColorsAsync(string color)
        {
            var colorLower = color.ToLower();
            var filter = Builders<Card>.Filter.Where(c => c.CardColors.ToLower().Contains(colorLower));
            return await _card.Find(filter).ToListAsync();
        }

        public async Task CreateAsync(Card card) => 
            await _card.InsertOneAsync(card);

        public async Task CreateMany(List<Card> cards) =>
            await _card.InsertManyAsync(cards);

        public async Task UpdateAsync(string id, Card newCard) => 
            await _card.ReplaceOneAsync(i => i.Id == id, newCard);

        public async Task RemoveAsync(string name) =>
            await _card.DeleteOneAsync(i => i.Name == name);

        public async Task RemoveAllAsync() =>
            await _card.DeleteManyAsync(Builders<Card>.Filter.Empty);
    }
}