using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string[]> GetStrings()
        {
            return Ok(new string[] { "something", "Mirza", "Dzeno" });
        }
    }
}
