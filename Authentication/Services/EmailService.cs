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

        public Task SendConfirmationEmail(User user)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }

            throw new NotImplementedException();
        }
    }
}
