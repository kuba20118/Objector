using Objector.Models;
using Objector.Repositories.Interfaces;
using Objector.Services.Interfaces;
using System;

namespace Objector.Services
{
    public class ImagesService : IImagesService
    {
        private readonly IImagesRepository _imagesRepository;

        public ImagesService(IImagesRepository imagesRepository)
        {
            _imagesRepository = imagesRepository;
        }

        public async Task<Guid> AddImageAsync(Result result)
        {
            var image = new ImageX(Guid.NewGuid(), result.ImageStringOriginal, result.ImageStringProcessed, result.Description, result.ElapsedTime);

            await _imagesRepository.AddAsync(image);

            return image.Id;
        }

        public async Task<IList<ImageX>> GetAllImagesAsync()
        {
            return await _imagesRepository.GetAllImagesAsync();
        }

        public Task<ImageX> GetImageAsync(Guid guid)
        {
            return _imagesRepository.GetImageAsync(guid);
        }
    }
}
