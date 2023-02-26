using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Requests;
using Models.DTOs.Responses;
using Models.Entities.Identity;
using Models.Interfaces.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            ILogger<AuthenticationController> logger, 
            IAuthenticationService authenticationService
            )
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<IdentityError>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserRegistrationResponseDTO>> RegisterAsync([FromBody] UserRegistrationRequestDTO request)
        {
            try
            {
                var result = await _authenticationService.RegisterAsync(request);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                return Ok(result.Response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
