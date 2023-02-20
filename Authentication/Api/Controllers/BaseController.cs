using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        [HttpGet]
        public string[] GetStrings()
        {
            return new string[] { "something", "Mirza", "Dzeno" };
        }
    }
}
