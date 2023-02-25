using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(
            ApplicationDbContext context,
            ILogger<AuthenticationController> logger
            )
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string[]> GetStrings()
        {
            return Ok(new string[] { "something", "Mirza", "Dzeno" });
        }
    }
}
