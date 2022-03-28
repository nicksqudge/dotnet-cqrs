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

        public static Result Failure()
        {
            return new Result(false);
        }

        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;

        public Result(bool isSuccess)
        {
            IsSuccess = true;
        }
    }

    public class Result<T> : Result
    {
        public Result(bool isSuccess) : base(isSuccess)
        {

        }
    }
}
