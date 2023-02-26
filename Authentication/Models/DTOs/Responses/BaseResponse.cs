using Models.DTOs.Error;

namespace Models.DTOs.Response
{
    public class BaseResponse : BaseException
    {
        public BaseResponse()
        {
            Succeeded = Errors is not null ? false : true;
        }

        public bool Succeeded { get; set; }
    }
}
