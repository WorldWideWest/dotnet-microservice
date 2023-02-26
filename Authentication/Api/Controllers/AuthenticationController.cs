using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            ILogger<AuthenticationController> logger
            )
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetStrings([FromBody]IdentityUser user)
        {
            _logger.LogInformation(user.ToString());
            return Ok();
        }
    }
}
