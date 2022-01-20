using Microsoft.AspNetCore.Mvc;
using Objector.Services;
using Objector.Services.Interfaces;

namespace Objector.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageMLService _imageMLService;

        public ImageController(            IImageMLService imageMLService            )
{
            _imageMLService = imageMLService;
        }

        [HttpPost]
        public async Task<IActionResult> IdentifyObjectsAsync([FromForm(Name = "Image")] IFormFile image)
        {
            await _imageMLService.IdentifyObjects(image, Guid.NewGuid());
            return Ok(DateTime.Now);
        }
    }
}
