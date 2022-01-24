using Objector.Models;

namespace Objector.Services.Interfaces
{
    public interface IImageService
    {
        Task<byte[]> GetImageAsync(Guid guid);
        Task<List<byte[]>> GetAllImagesAsync();
        Task AddImageAsync(Result result);
    }
}
