namespace Resolvr
{
    [System.Serializable]
    public class ResultException : System.Exception
    {
        public ResultException() { }
        public ResultException(string message) : base(message) { }
        public ResultException(string message, System.Exception inner) : base(message, inner) { }
        protected ResultException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [System.Serializable]
    public class UnwrapException : ResultException
    {
        public UnwrapException() { }
        public UnwrapException(string message) : base(message) { }
        public UnwrapException(string message, System.Exception inner) : base(message, inner) { }
        protected UnwrapException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}