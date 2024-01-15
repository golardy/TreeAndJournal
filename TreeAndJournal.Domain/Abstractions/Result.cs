using System;

namespace TreeAndJournal.Domain.Abstractions
{
    public class Result
    {
        public bool IsSuccess { get; }

        public string Error { get; }

        protected internal Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, string.Empty);
        public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, string.Empty);
        public static Result<TValue> Failure<TValue>(string error) => new Result<TValue>(default, false, error);

        public static Result<TValue> Create<TValue>(TValue value) =>
            value != null ? Success(value) : Failure<TValue>("The value can not be processed");
    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        protected internal Result(TValue value, bool isSuccess, string error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public static implicit operator Result<TValue>(TValue value) => Create(value);
    }
}
