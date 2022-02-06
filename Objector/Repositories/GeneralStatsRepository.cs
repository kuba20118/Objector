using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Objector.Models;
using Objector.Mongo;
using Objector.Repositories.Interfaces;

namespace Objector.Repositories
{
    public class GeneralStatsRepository : IGeneralStatsRepository
    {
        private readonly IMongoCollection<GeneralStats> _stats;

        public GeneralStatsRepository(IOptions<MongoSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.Database);

            _stats = mongoDatabase.GetCollection<GeneralStats>("GeneralStats");
        }

        public async Task CreateAsync() => await _stats.InsertOneAsync(new GeneralStats());

        public async Task<GeneralStats> GetAsync() => await _stats.AsQueryable().FirstOrDefaultAsync();

        public async Task UpdateAsync(Feedback feedback, int numberOfObjects, IList<string> foundObjects, long time)
        {
            var stats = await GetAsync();

            //from feedback
            stats.CorrectObjectsDetections += feedback.Correct;
            stats.NotFoundObjects += feedback.NotFound;
            stats.IncorrectObjectsDetections += feedback.Incorrect;
            stats.MultipleObjectsDetections += feedback.MultipleFound;
            stats.IncorrectBoxDetections += feedback.IncorrectBox;

            //from ml
            stats.ObjectsFoundByML += numberOfObjects;
            stats.Time += time;
            foreach (var obj in foundObjects)
            {
                if (stats.ObjectsFound.ContainsKey(obj))
                    stats.ObjectsFound[obj]++;
                else
                    stats.ObjectsFound.Add(obj, 1);
            }
            stats.ObjectsFound.OrderBy(x => x.Value);

            //rest
            stats.SmallMistakes += (feedback.MultipleFound + feedback.IncorrectBox);
            stats.CriticalMistakes += (feedback.NotFound + feedback.Incorrect);
            stats.AllMistakes += (feedback.MultipleFound + feedback.IncorrectBox + feedback.NotFound + feedback.Incorrect);
            stats.Detections++;
            stats.AverageTime = stats.Time / stats.Detections;

            await _stats.ReplaceOneAsync(x => x.Key == "GeneralStats", stats);
        }
    }
}
