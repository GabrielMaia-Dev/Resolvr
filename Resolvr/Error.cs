namespace Resolvr
{
    public class Error
    {
        /// <summary>
        /// Error message
        /// </summary>
        public virtual string Message { get; }

        /// <summary>
        /// Create a error with a optional message.
        /// </summary>
        /// <param name="message"></param>
        public Error(string? message = null)
        {
            Message = message ?? string.Empty;
        }

        public static implicit operator Error(string value)
        => new Error(value);

        public static implicit operator Result(Error error)
        => new Result(error);

    }
}
