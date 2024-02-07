using System.Diagnostics.CodeAnalysis;

namespace Resolvr
{
    public class Result
    {
        [MemberNotNullWhen(true, nameof(ObjectValue))]
        [MemberNotNullWhen(false, nameof(Error))]
        public virtual bool IsSuccess { get => Error is null; }
        [MemberNotNullWhen(false, nameof(ObjectValue))]
        [MemberNotNullWhen(true, nameof(Error))]
        public virtual bool IsError { get => !IsSuccess; }
        public Error? Error { get; set; }
        public object? ObjectValue { get; protected set; }

        public Result(object? obj = null)
        {
            ObjectValue = obj;
        }
        public Result(Error? error)
        {
            Error = error;
        }

        public static Result<T> Ok<T>()
            => new Result<T>();
        public static Result<T> Ok<T>(T obj)
            => new Result<T>(obj);
        public static Result Ok()
            => new Result();
        public static Result<T> ErrorIf<T>(bool condition, Error? error = null)
            => condition ? (error ?? new Error()) : Ok<T>();
        public static Result ErrorIf(bool condition, Error? error = null)
            => condition ? (error ?? new Error()) : Ok();
    }

    public class Result<T> : Result
    {
        [MemberNotNullWhen(true, nameof(Value))]
        public override bool IsSuccess => base.IsSuccess;
        [MemberNotNullWhen(false, nameof(Value))]
        public override bool IsError => base.IsError;
        public T? Value { get; }

        public Result() : base() {}
        public Result(T value) : base(value)
        {
            Value = value;
        }

        public Result(Error error) : base(error) { }

        public static implicit operator Result<T>(T value)
        => new Result<T>(value);

        public static implicit operator Result<T>(Error error)
        => new Result<T>(error);

        public static implicit operator T?(Result<T> value)
        => value.Value;
    }
}