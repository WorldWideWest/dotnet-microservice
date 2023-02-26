using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Models.DTOs.Requests;
using Models.DTOs.Response;
using Models.DTOs.Responses;
using Models.Entities.Identity;
using Models.Interfaces.Services;

namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;

        public AuthenticationService(
            UserManager<User> userManager,
            ILogger<AuthenticationService> logger,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserRegistrationResponseDTO> Register(UserRegistrationRequestDTO request)
        {
            try
            {
                User user = _mapper.Map<UserRegistrationRequestDTO, User>(request);

                await _userManager.CreateAsync(user);

                return new()
                {
                    Message = "User Successfully Created, please check your Email"
                };
            }
            catch (Exception ex)
            {

                throw ;
            }
        }
    }
}
