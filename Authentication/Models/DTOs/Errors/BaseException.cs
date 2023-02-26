namespace Models.DTOs.Error
{
    public class BaseException
    {
        public BaseException()
        {
        }

        public BaseException(List<Error> errors)
        {
            Errors = errors;
        }

        public BaseException(Error error)
        {
            Errors.Add(error);
        }

        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
