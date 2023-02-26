using Microsoft.AspNetCore.Identity;

namespace Models.DTOs.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Succeeded = Errors is null;
        }

        public List<IdentityError> Errors { get; set; }
        public Response Response { get; set; }
        public bool Succeeded { get; set; }
    }
   
    public class Response
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
