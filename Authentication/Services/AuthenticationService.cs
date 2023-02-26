using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Models.Constants.Error;
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
        private readonly IEmailService _emailService;

        public AuthenticationService(
            UserManager<User> userManager,
            ILogger<AuthenticationService> logger,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IEmailService emailService
            )
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }

        public async Task<UserRegistrationResponseDTO> RegisterAsync(UserRegistrationRequestDTO request)
        {
            try
            {
                var user = await _emailService.FindUserByEmail(request.Email);
                if (user != null)
                {
                    IdentityError userExistsError = new()
                    {
                        Code = StatusCodes.Status409Conflict.ToString(),
                        Description = ErrorMessages.USER_ALREADY_EXISTS
                    };

                    return new(userExistsError);
                }

                User newUser = _mapper.Map<UserRegistrationRequestDTO, User>(request);
                newUser.PasswordHash = _passwordHasher.HashPassword(newUser, request.Password);
                
                var result = await _userManager.CreateAsync(newUser);
                if (!result.Succeeded)
                    return new() { Errors = result.Errors.ToList() };

                Response response = new()
                {
                    Description = SuccessMessages.CONFIRM_EMAIL,
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
