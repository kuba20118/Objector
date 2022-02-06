using Microsoft.AspNetCore.Mvc;
using Objector.Models;
using Objector.Models.Charts;
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
        private readonly IStatsService _statsService;

        public ImageController(IImageMLService imageMLService, IImagesService imageService, IStatsService statsService)
        {
            _imageMLService = imageMLService;
            _imageService = imageService;
            _statsService = statsService;
        }

        [HttpPost]
        public async Task<IActionResult> IdentifyObjectsAsync([FromForm(Name = "Image")] IFormFile image)
        {
            var guid = await _imageMLService.IdentifyObjectsAsync(image);
            var result = await _imageService.GetImageAsync(guid);

            return Ok(result);
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

        [HttpPost("stats")]
        public async Task<IActionResult> AddStatsToImage(AddStats userStats)
        {
            var stats = new Feedback(userStats.Correct, userStats.Incorrect, userStats.NotFound, userStats.MultipleFound, userStats.IncorrectBox);
            await _statsService.AddStatsToImage(userStats.ImageId, stats);
            await _statsService.UpdateGeneralStats(userStats.ImageId, stats);

            var imageStats = await _statsService.GetImageStatsAsync(userStats.ImageId);

            return Ok(imageStats);
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetSummaryStats()
        {
            var statsToReturn = await _statsService.GetSummaryStats();

            return Ok(statsToReturn);
        }

        [HttpGet("stats/{id}")]
        public async Task<IActionResult> GetImageStatsAsync(Guid id)
        {
            var imageStatsToReturn = await _statsService.GetImageStatsAsync(id);

            return Ok(imageStatsToReturn);
        }
    }
}
