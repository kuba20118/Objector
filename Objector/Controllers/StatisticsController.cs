using Microsoft.AspNetCore.Mvc;

namespace Objector.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        public StatisticsController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentDate()
        {
            return Ok(DateTime.Now);
        }
    }
}
