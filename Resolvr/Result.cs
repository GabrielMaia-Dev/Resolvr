using System.Diagnostics.CodeAnalysis;

namespace Resolvr
{
    public class Result
    {
        /// <summary>
        /// True if is a success. 'Error' will not be not null if this flag is set to false.
        /// </summary>
        [MemberNotNullWhen(false, nameof(ErrorValue))]
        public virtual bool IsSuccess { get => ErrorValue is null; }
        /// <summary>
        /// True if is a error. 'Error' will not be not null if this flag is set to true.
        /// </summary>
        [MemberNotNullWhen(true, nameof(ErrorValue))]
        public virtual bool IsError { get => !IsSuccess; }

        /// <summary>
        /// Undelying Error value, not null if IsSuccess == false or IsError == true
        /// </summary>
        public Error? ErrorValue { get; set; }

        internal Result(){ }

        internal Result(Error? error = null)
        {
            ErrorValue = error ?? new Error();
        }
        
        /// <summary>
        /// Create a success result with value.
        /// </summary>
        public static Result<T> Ok<T>(T obj)
            => new Result<T>(obj);

        /// <summary>
        /// Create a success result with no value.
        /// </summary>
        public static Result Ok()
            => new Result();

        /// <summary>
        /// Create a success result with value.
        /// </summary>
        public static Result<T> Error<T>(Error? error = null)
            => new Result<T>(error ?? new Error());

        /// <summary>
        /// Create a success result with no value.
        /// </summary>
        public static Result Error(Error? error = null)
            => new Result(error ?? new Error());

        // /// <summary>
        // /// Create a success if condition is true, with an optional error
        // /// </summary>
        // public static Result OkIf(bool condition, Error? error = null)
        //     => condition ? new Result() : (error ?? new Error());
        
        // /// <summary>
        // /// Create a error if condition is true, with an optional error
        // /// </summary>
        // public static Result ErrorIf(bool condition, Error? error = null)
        //     => condition ? (error ?? new Error()) : Ok();
    }


    public class Result<T> : Result
    {
        /// <summary>
        /// True if is a success. 'Value' will not be not null if this flag is set to true.
        /// </summary>
        [MemberNotNullWhen(true, nameof(Value))]
        public override bool IsSuccess => base.IsSuccess;
        /// <summary>
        /// True if is a success. 'Error' will not be not null if this flag is set to true.
        /// </summary>
        [MemberNotNullWhen(false, nameof(Value))]
        public override bool IsError => base.IsError;
        /// <summary>
        /// Underlying value, non null if IsSuccess == true or IsError == false
        /// </summary>
        public T? Value { get; }

        internal Result(T value) : base() { Value = value; }

        internal Result(Error error) : base(error) { }

        public static implicit operator Result<T>(T value)
        => new Result<T>(value);

        public static implicit operator Result<T>(Error error)
        => new Result<T>(error);

        public static implicit operator T?(Result<T> value)
        => value.Value;
    }
}