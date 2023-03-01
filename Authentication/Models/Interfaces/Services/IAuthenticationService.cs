using Microsoft.AspNetCore.Identity;
using Models.DTOs.Requests;
using Models.DTOs.Responses;

namespace Models.Interfaces.Services
{
    public interface IAuthenticationService
    {
        public Task<IdentityResult> RegisterAsync(UserRegistrationRequestDTO request);
        public Task<IdentityResult> VerifyEmailAsync(EmailVerificationRequestDTO request);
    }
}
