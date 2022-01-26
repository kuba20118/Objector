using Microsoft.AspNetCore.Mvc;
using Objector.Services;
using Objector.Services.Interfaces;

namespace Objector.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageMLService _imageMLService;
        private readonly IImagesService _imageService;

        public ImageController(IImageMLService imageMLService, IImagesService imageService)
        {
            _imageMLService = imageMLService;
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> IdentifyObjectsAsync([FromForm(Name = "Image")] IFormFile image)
        {
            return Ok(await _imageMLService.IdentifyObjectsAsync(image));
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(Guid guid)
        {
            var image = await _imageService.GetImageAsync(guid);

            return Ok(image);
        }

        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            var images = await _imageService.GetAllImagesAsync();

            return Ok(images);
        }
    }
}
