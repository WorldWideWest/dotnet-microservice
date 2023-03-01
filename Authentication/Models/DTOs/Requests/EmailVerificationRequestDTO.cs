using Models.Constants.Error;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Requests
{
    public class EmailVerificationRequestDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = ErrorMessages.INVALID_EMAIL)]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
