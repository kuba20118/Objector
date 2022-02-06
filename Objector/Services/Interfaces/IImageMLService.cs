namespace Objector.Services.Interfaces
{
    public interface IImageMLService
    {
        Task<Guid> IdentifyObjectsAsync(IFormFile imageFile);
    }
}
