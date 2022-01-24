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

        public async Task AddImageAsync(Result result)
{
            var image = new ImageX(Guid.NewGuid(), result.ImageStringOriginal, result.ImageStringProcessed, result.Description, result.ElapsedTime);

            await _imagesRepository.AddAsync(image);
        }

        public async Task<IList<ImageX>> GetAllImagesAsync()
        {
            return await _imagesRepository.GetAllImagesAsync();
        }

        public async Task<ImageX> GetImageAsync(Guid guid)
        {
            return await _imagesRepository.GetImageAsync(guid);
        }
    }
}
