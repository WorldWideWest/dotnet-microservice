using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Models.Entities.Identity;
using Models.Interfaces.Services;

namespace Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly UserManager<User> _userManager;

        public EmailService(ILogger<EmailService> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }


        public async Task SendConfirmationEmail(User user)
        {
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var verificationUrl = Url.Action
            }
            catch (Exception ex)
            {

                throw ex;
            }

            throw new NotImplementedException();
        }

        public async Task<User> FindUserByEmail(string email)
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
