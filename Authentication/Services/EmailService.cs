using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Models.Entities.Identity;
using Models.Interfaces.Services;

namespace Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly HttpRequest _request;
        private readonly IUrlHelper _urlHelper;
        public EmailService(
            ILogger<EmailService> logger,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper)
        {
            _logger = logger;
            _userManager = userManager;
            _request = httpContextAccessor.HttpContext.Request;
            _urlHelper = urlHelper;
        }


        public async Task SendConfirmationEmail(string email)
        {
            try
            {
                User user = await FindUserByEmailAsync(email);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                UrlActionContext urlActionContext = new()
                {
                    Action = "confirm",
                    Controller = "Authentication",
                    Host = _request.Host.ToString(),
                    Values = new { email, token }
                };

                var callbackUrl = _urlHelper.Action(urlActionContext);
                

                _logger.LogInformation(callbackUrl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            throw new NotImplementedException();
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            try
            {
                var result = await _userManager.FindByEmailAsync(email);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
