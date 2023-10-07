using Microsoft.AspNetCore.Mvc;
using Shared.Requests;

namespace Shared.Controllers
{
    [ApiController]
    [Route("hc")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get() => Ok(new Response<object>());
    }
}