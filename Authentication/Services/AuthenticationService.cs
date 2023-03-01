using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Constants.Email;
using Models.Constants.Error;
using Models.DTOs.Requests;
using Models.DTOs.Responses;
using Models.Entities.Identity;
using Models.Interfaces.Services;
using NETCore.MailKit.Core;

namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly HttpRequest _request;
        private readonly IUrlHelper _urlHelper;
        private readonly IEmailService _emailService;

        public AuthenticationService(
            UserManager<User> userManager,
            ILogger<AuthenticationService> logger,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper,
            IEmailService emailService
            )
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _request = httpContextAccessor.HttpContext.Request;
            _urlHelper = urlHelper;
            _emailService = emailService;
        }

        public async Task<IdentityResult> RegisterAsync(UserRegistrationRequestDTO request)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(request.Email);
                if (userExists is not null)
                {
                    IdentityError error = new()
                    {
                        Code = StatusCodes.Status409Conflict.ToString(),
                        Description = ErrorMessages.USER_ALREADY_EXISTS
                    };

                    return IdentityResult.Failed(error);
                }

                User newUser = _mapper.Map<UserRegistrationRequestDTO, User>(request);
                newUser.PasswordHash = _passwordHasher.HashPassword(newUser, request.Password);
                
                var result = await _userManager.CreateAsync(newUser).ConfigureAwait(false);
                if (!result.Succeeded)
                    return result;
                
                User user = await _userManager.FindByEmailAsync(request.Email);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                string callbackUrl = $"{_request.Scheme}://{_request.Host}/api/Authentication/verify?version=1.0/email={user.Email}&token={token}";
                
                await _emailService.SendAsync(
                    newUser.Email, 
                    Subjects.EMAIL_VERIFICATION, 
                    EmailMessages.Verification(callbackUrl),
                    true);

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {

                throw ;
            }
        }

        public async Task<IdentityResult> VerifyEmailAsync(EmailVerificationRequestDTO request)
        {
            try
            {
                User user = await _userManager.FindByEmailAsync(request.Email);
                if(user is null)
                {
                    IdentityError error = new() { 
                        Code = StatusCodes.Status404NotFound.ToString(),
                        Description = ErrorMessages.USER_NOT_FOUND
                    };

                    return IdentityResult.Failed(error);
                }

                var result = await _userManager.ConfirmEmailAsync(user, request.Token);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
