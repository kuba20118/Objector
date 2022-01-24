using Objector.Models;
using Objector.Repositories.Interfaces;

namespace Objector.Repositories
{
    public class ImagesRepository : IImagesRepository
    {
        public ImagesRepository()
        {

        }

        public Task AddAsync(ImageX image)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ImageX>> GetAllImagesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ImageX> GetImageAsync(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
