using GamesApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GamesApi.Services
{
    public class CharacterService
    {
        private readonly IMongoCollection<Character> _charactersCollection;

        public CharacterService(
            IOptions<DatabaseSettings> characterDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                characterDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                characterDatabaseSettings.Value.DatabaseName);

            _charactersCollection = mongoDatabase.GetCollection<Character>(
                characterDatabaseSettings.Value.CharactersCollectionName);
        }

        public List<Character> Get()
        {
            return _charactersCollection.Find(_ => true).ToList();
        }

        public async Task<List<Character>> GetAsync() =>
            await _charactersCollection.Find(_ => true).ToListAsync();

        public async Task<Character?> GetAsync(string id) =>
            await _charactersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Character newCharacter) =>
            await _charactersCollection.InsertOneAsync(newCharacter);

        public async Task UpdateAsync(string id, Character updatedCharacter) =>
            await _charactersCollection.ReplaceOneAsync(x => x.Id == id, updatedCharacter);

        public async Task RemoveAsync(string id) =>
            await _charactersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
