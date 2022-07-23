namespace FinLib.Common.Exceptions.Base
{
    /// <summary>
    /// base class for any infrastructure-related exception classes.
    /// this class is abstract
    /// </summary>
    [Serializable]
    public abstract class BaseInfraException : BaseException
    {
        protected BaseInfraException() { }

        protected BaseInfraException(string message)
            : base(message) { }

        protected BaseInfraException(string message, Exception inner)
            : base(message, inner) { }

        protected BaseInfraException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
