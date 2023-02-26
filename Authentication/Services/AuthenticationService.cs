using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Models.Entities.Identity;

namespace Services
{
    public class AuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            UserManager<User> userManager,
            ILogger<AuthenticationService> logger
            )
        {
            _userManager = userManager;
            _logger = logger;
        }

        
    }
}
