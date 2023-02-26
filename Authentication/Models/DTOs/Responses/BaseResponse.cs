using Models.DTOs.Error;

namespace Models.DTOs.Response
{
    public class BaseResponse : BaseException
    {
        public BaseResponse()
        {
            IsSuccessful = Errors is not null ? false : true;
        }

        public bool IsSuccessful { get; set; }
    }
}
