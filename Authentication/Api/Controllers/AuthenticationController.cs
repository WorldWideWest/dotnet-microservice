using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Requests;
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
        [ProducesResponseType(typeof(IdentityResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<IdentityError>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdentityResult>> RegisterAsync([FromBody] UserRegistrationRequestDTO request)
        {
            try
            {
                var result = await _authenticationService.RegisterAsync(request)
                    .ConfigureAwait(false);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(RegisterAsync));
                throw ex;
            }
        }
        
        [HttpPost("verify")]
        [ProducesResponseType(typeof(IdentityResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<IdentityError>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdentityResult>> VerifyEmailAsync([FromBody] EmailVerificationRequestDTO request)
        {
            try
            {
                var result = await _authenticationService.VerifyEmailAsync(request)
                    .ConfigureAwait(false);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(VerifyEmailAsync));
                throw ex;
            }
        }
    }
}
