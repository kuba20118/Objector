using Microsoft.AspNetCore.Mvc;

namespace Objector.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentDate()
        {
            return Ok(DateTime.Now);
        }
    }
}
