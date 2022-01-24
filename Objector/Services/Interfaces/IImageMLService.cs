namespace Objector.Services.Interfaces
{
    public interface IImageMLService
    {
        Task<byte[]> IdentifyObjectsAsync(IFormFile imageFile);
    }
}
