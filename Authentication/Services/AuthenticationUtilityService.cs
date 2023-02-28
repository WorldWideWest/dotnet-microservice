using Microsoft.Extensions.Logging;
using Models.DTOs.General;
using Models.Interfaces.Services;
using NETCore.MailKit.Core;

namespace Services
{
    public class AuthenticationUtilityService : IAuthenticationUtilityService
    {
		private readonly ILogger<AuthenticationUtilityService> _logger;
		private readonly IEmailService _emailService;

		public AuthenticationUtilityService(
			ILogger<AuthenticationUtilityService> logger,
			IEmailService emailService
			)
		{
			_logger = logger;
			_emailService = emailService;
		}

        public void SendActionEmail(ActionEmailContext actionEmailContext)
        {
			try
			{
				_emailService.SendAsync(
					actionEmailContext.MailTo,
					actionEmailContext.Subject,
					actionEmailContext.Message,
					actionEmailContext.IsHtml);
			}
            catch (Exception ex)
			{

				throw ex;
			}
        }
    }
}
