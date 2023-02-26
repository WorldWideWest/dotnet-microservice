using Microsoft.AspNetCore.Identity;

namespace Models.DTOs.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Succeeded = Errors.Count == 0;
        }

        public BaseResponse(List<IdentityError> errors) => Errors.AddRange(errors);

        public BaseResponse(IdentityError error) => Errors.Add(error);

        public List<IdentityError> Errors { get; set; } =  new List<IdentityError>();
        public Response Response { get; set; }
        public bool Succeeded { get; set; }
    }
   
    public class Response
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
