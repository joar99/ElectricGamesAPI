using GamesApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Policy;

namespace GamesApi.Services
{
    public class GamesService
    {
        private readonly IMongoCollection<Game> _gamesCollection;

        public GamesService(
            IOptions<DatabaseSettings> gameDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                gameDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                gameDatabaseSettings.Value.DatabaseName);

            _gamesCollection = mongoDatabase.GetCollection<Game>(
                gameDatabaseSettings.Value.GamesCollectionName);
        }

        public List<Game> Get()
        {
            return _gamesCollection.Find(_ => true).ToList();
        }

        public async Task<List<Game>> GetAsync() =>
            await _gamesCollection.Find(_ => true).ToListAsync();

        public async Task<Game?> GetAsync(string id) =>
            await _gamesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Game newGame) =>
            await _gamesCollection.InsertOneAsync(newGame);

        public async Task UpdateAsync(string id, Game updatedGame) =>
            await _gamesCollection.ReplaceOneAsync(x => x.Id == id, updatedGame);

        public async Task RemoveAsync(string id) =>
            await _gamesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
