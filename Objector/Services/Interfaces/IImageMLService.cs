namespace Objector.Services.Interfaces
{
    public interface IImageMLService
    {
        Task IdentifyObjects(IFormFile imageFile, Guid id);
    }
}
