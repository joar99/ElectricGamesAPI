using GameApiTwo.Models;
using GamesApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GameApiTwo.Services
{
        public class DeveloperService
        {
            private readonly IMongoCollection<Developer> _developersCollection;

            public DeveloperService(
                IOptions<DatabaseSettings> developerDatabaseSettings)
            {
                var mongoClient = new MongoClient(
                    developerDatabaseSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                    developerDatabaseSettings.Value.DatabaseName);

                _developersCollection = mongoDatabase.GetCollection<Developer>(
                    developerDatabaseSettings.Value.DevelopersCollectionName);
            }

            public List<Developer> Get()
            {
                return _developersCollection.Find(_ => true).ToList();
            }

        public async Task<List<Developer>> GetAsync() =>
                await _developersCollection.Find(_ => true).ToListAsync();

            public async Task<Developer?> GetAsync(string id) =>
                await _developersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            public async Task CreateAsync(Developer newDeveloper) =>
                await _developersCollection.InsertOneAsync(newDeveloper);

            public async Task UpdateAsync(string id, Developer updatedDeveloper) =>
                await _developersCollection.ReplaceOneAsync(x => x.Id == id, updatedDeveloper);

            public async Task RemoveAsync(string id) =>
                await _developersCollection.DeleteOneAsync(x => x.Id == id);
        }
    }

