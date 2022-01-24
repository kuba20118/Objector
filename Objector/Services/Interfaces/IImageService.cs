namespace Objector.Services.Interfaces
{
    public interface IImageService
    {
        Task<byte[]> GetImageAsync(Guid guid);
        Task<List<byte[]>> GetAllImagesAsync();
    }
}
