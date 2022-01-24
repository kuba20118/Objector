using Objector.Models;

namespace Objector.Services.Interfaces
{
    public interface IImagesService
    {
        Task<ImageX> GetImageAsync(Guid guid);
        Task<IList<ImageX>> GetAllImagesAsync();
        Task AddImageAsync(Result result);
    }
}
