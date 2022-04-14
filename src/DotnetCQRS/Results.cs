namespace DotnetCQRS
{
    public class Result
    {
        public Result(bool isSuccess, string errorCode = "")
        {
            IsSuccess = isSuccess;
            ErrorCode = errorCode;
        }

        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;
        public string ErrorCode { get; private set; } = string.Empty;

        public static Result Success()
        {
            return new Result(true);
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(true, value);
        }

        public static Result<T> EmptySuccess<T>()
        {
            return new Result<T>(true);
        }
        
        public static Result Failure(string errorCode = "UNKNOWN")
        {
            return new Result(false, errorCode);
        }

        public static Result<T> Failure<T>(string errorCode = "UNKNOWN")
        {
            return new Result<T>(false, errorCode);
        }
    }

    public class Result<T> : Result
    {
        public Result(bool isSuccess, string errorCode = "") : base(isSuccess, errorCode)
        {
            Value = default;
            HasValue = false;
        }

        public Result(bool isSuccess, T value, string errorCode = "") : base(isSuccess, errorCode)
        {
            Value = value;
            HasValue = true;
        }

        public T Value { get; private set; }
        
        public bool HasValue { get; private set; }
    }
}