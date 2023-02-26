using Microsoft.AspNetCore.Identity;

namespace Models.DTOs.Responses
{
    public class UserRegistrationResponseDTO : BaseResponse
    {
        public UserRegistrationResponseDTO()
        {

        }

        public UserRegistrationResponseDTO(List<IdentityError> errors) : base(errors)
        {

        }

        public UserRegistrationResponseDTO(IdentityError error) : base(error)
        {

        }
    }
}
