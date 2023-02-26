
using Models.Errors.Constants;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Request
{
    public class UserRegistrationDTO
    {
        [Required(ErrorMessage = Messages.PROPERTY_REQUIRED)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = Messages.PROPERTY_REQUIRED)]
        public string LastName { get; set; }

        [Required(ErrorMessage = Messages.PROPERTY_REQUIRED)]
        public string Email { get; set; }

        [Required(ErrorMessage = Messages.PROPERTY_REQUIRED)]
        public string Password { get; set; }

    }
}
