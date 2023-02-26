namespace Models.DTOs.Error
{
    public class BaseException
    {
        public BaseException()
        {
            IsSuccessful = Errors.Count == 0;
        }

        public BaseException(List<Error> errors)
        {
            Errors = errors;
            IsSuccessful = Errors.Count == 0;
        }

        public BaseException(Error error)
        {
            Errors.Add(error);
            IsSuccessful = Errors.Count == 0;
        }

        public bool IsSuccessful { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
