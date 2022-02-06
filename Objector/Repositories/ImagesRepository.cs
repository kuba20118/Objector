using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Objector.Models;
using Objector.Mongo;
using Objector.Repositories.Interfaces;

namespace Objector.Repositories
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly IMongoCollection<ImageX> _images;

        public ImagesRepository(IOptions<MongoSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.Database);

            _images = mongoDatabase.GetCollection<ImageX>("Images");
        }

        public async Task AddAsync(ImageX image) => await _images.InsertOneAsync(image);

        public async Task<IList<ImageX>> GetAllImagesAsync() => await _images.AsQueryable().ToListAsync();

        public async Task<ImageX> GetImageAsync(Guid guid) => await _images.AsQueryable().FirstOrDefaultAsync(x => x.Id == guid);
    }
}
