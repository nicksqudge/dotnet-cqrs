using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCQRS
{
    public class Result
    {
        public static Result Success()
        {
            return new Result(true);
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(true, value);
        }

        public static Result Failure(string errorCode="UNKNOWN")
        {
            return new Result(false, errorCode);
        }
        
        public static Result<T> Failure<T>(string errorCode="UNKNOWN")
        {
            return new Result<T>(false, errorCode);
        }

        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public string ErrorCode { get; set; } = string.Empty;

        public Result(bool isSuccess, string errorCode="")
        {
            IsSuccess = isSuccess;
            ErrorCode = errorCode;
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }

        public Result(bool isSuccess, string errorCode="") : base(isSuccess, errorCode)
        {
            Value = default(T);
        }

        public Result(bool isSuccess, T value, string errorCode = "") : base(isSuccess, errorCode)
        {
            Value = value;
        }
    }
}
