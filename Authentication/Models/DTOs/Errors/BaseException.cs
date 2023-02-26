using Microsoft.AspNetCore.Identity;

namespace Models.DTOs.Error
{
    public class BaseException
    {
        public BaseException()
        {
        }

        public BaseException(List<IdentityError> errors)
        {
            Errors = errors;
        }

        public BaseException(IdentityError error)
        {
            Errors.Add(error);
        }

        public List<IdentityError> Errors { get; set; }
    }
}
