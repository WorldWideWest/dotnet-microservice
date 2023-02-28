using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Models.Constants.Email;
using Models.Constants.Error;
using Models.Constants.Success;
using Models.DTOs.General;
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
        private readonly HttpRequest _request;
        private readonly IAuthenticationUtilityService _authenticationUtilityService;
        private readonly IUrlHelper _urlHelper;

        public AuthenticationService(
            UserManager<User> userManager,
            ILogger<AuthenticationService> logger,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationUtilityService authenticationUtilityService,
            IUrlHelper urlHelper
            )
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _request = httpContextAccessor.HttpContext.Request;
            _authenticationUtilityService = authenticationUtilityService;
            _urlHelper = urlHelper;
        }

        public async Task<UserRegistrationResponseDTO> RegisterAsync(UserRegistrationRequestDTO request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
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
                
                var result = await _userManager.CreateAsync(newUser).ConfigureAwait(false);
                if (!result.Succeeded)
                    return new() { Errors = result.Errors.ToList() };

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                UrlActionContext urlActionContext = new()
                {
                    Action = "confirm",
                    Controller = "Authentication",
                    Host = _request.Host.ToString(),
                    Values = new { email = newUser.Email, token }
                };

                var callbackUrl = _urlHelper.Action(urlActionContext);
                
                ActionEmailContext actionEmailContext = new()
                {
                    MailTo = newUser.Email,
                    Subject = Subjects.EMAIL_VERIFICATION,
                    Message = EmailMessages.Verification(newUser.Email),
                    IsHtml = true
                };

                _authenticationUtilityService.SendActionEmail(actionEmailContext);

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
