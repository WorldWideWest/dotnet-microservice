using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Models.Constants.Success;
using Models.DTOs.Requests;
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
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthenticationService(
            UserManager<User> userManager,
            ILogger<AuthenticationService> logger,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher
            )
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserRegistrationResponseDTO> RegisterAsync(UserRegistrationRequestDTO request)
        {
            try
            {
                User user = _mapper.Map<UserRegistrationRequestDTO, User>(request);
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                    return new() { Errors = result.Errors.ToList() };

                Response response = new()
                {
                    Message = Messages.CONFIRM_EMAIL,
                    Code = StatusCodes.Status201Created.ToString()
                };

                return new() { Response = response };
            }
            catch (Exception ex)
            {

                throw ;
            }
        }
    }
}
