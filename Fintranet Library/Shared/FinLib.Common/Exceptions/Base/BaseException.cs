namespace FinLib.Common.Exceptions.Base
{
    [Serializable]
    public abstract class BaseException : Exception
    {
        protected BaseException() 
        {
            TraceId = Guid.NewGuid();
        }

        protected BaseException(string message)
            : base(message) 
        {
            TraceId = Guid.NewGuid();
        }

        protected BaseException(string message, Exception inner)
            : base(message, inner) 
        {
            TraceId = Guid.NewGuid();
        }

        protected BaseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) 
        {
            TraceId = Guid.NewGuid();
        }

        public Guid TraceId { get; }
    }
}
