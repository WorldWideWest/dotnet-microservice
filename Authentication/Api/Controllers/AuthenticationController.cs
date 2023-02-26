using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Request;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Register([FromBody]UserRegistrationDTO request)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
