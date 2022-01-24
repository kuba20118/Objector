using Objector.Models;
using Objector.Services.Interfaces;

namespace Objector.Services
{
    public class ImageService : IImageService
    {
        public async Task AddImageAsync(Result result)
        {
        }

        public async Task<List<byte[]>> GetAllImagesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> GetImageAsync(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
