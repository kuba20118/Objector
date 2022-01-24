using Objector.Models;

namespace Objector.Repositories.Interfaces
{
    public interface IImagesRepository
    {
        Task AddAsync(ImageX image);
        Task<IList<ImageX>> GetAllImagesAsync();
        Task<ImageX> GetImageAsync(Guid guid);
    }
}
