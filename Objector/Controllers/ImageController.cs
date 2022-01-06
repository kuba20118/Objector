using Microsoft.AspNetCore.Mvc;

namespace Objector.API.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public ImageController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentDate()
        {
            return Ok(DateTime.Now);
        }
    }
}
