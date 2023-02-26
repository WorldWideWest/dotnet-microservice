using Models.Constants.Error;
using Models.Constants.Regex;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Requests
{
    public class UserRegistrationRequestDTO
    {
        [Required(ErrorMessage = ErrorMessages.PROPERTY_REQUIRED)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = ErrorMessages.PROPERTY_REQUIRED)]
        public string LastName { get; set; }

        [Required(ErrorMessage = ErrorMessages.PROPERTY_REQUIRED)]
        [EmailAddress(ErrorMessage = ErrorMessages.INVALID_EMAIL)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessages.PROPERTY_REQUIRED)]
        public string Password { get; set; }

    }
}
