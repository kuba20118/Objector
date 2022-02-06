using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Objector.Models;
using Objector.Mongo;
using Objector.Repositories.Interfaces;

namespace Objector.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly IMongoCollection<Statistics> _stats;

        public StatsRepository(IOptions<MongoSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.Database);

            _stats = mongoDatabase.GetCollection<Statistics>("Stats");
        }

        public async Task AddAsync(Statistics statistics)
           => await _stats.InsertOneAsync(statistics);

        public async Task<IEnumerable<Statistics>> GetAllAsync()
            => await _stats.AsQueryable().ToListAsync();

        public async Task<Statistics> GetAsync(Guid id)
            => await _stats.AsQueryable().FirstOrDefaultAsync(x => x.ImageId == id);
    }
}
