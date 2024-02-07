namespace Resolvr
{
    public class Error
    {
        public virtual string Message { get; }
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
