using Models.DTOs.Requests;
using Models.DTOs.Response;
using Models.DTOs.Responses;

namespace Models.Interfaces.Services
{
    public interface IAuthenticationService
    {
        public Task<UserRegistrationResponseDTO> RegisterAsync(UserRegistrationRequestDTO request);
    }
}
