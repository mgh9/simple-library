using System;

namespace FinLib.Common.Exceptions.Base
{
    /// <summary>
    /// base class for any business-related exceptions classes.
    /// this class is abstract
    /// </summary>
    [Serializable]
    public abstract class BaseBusinessException : BaseException
    {
        protected BaseBusinessException() { }

        protected BaseBusinessException(string message)
            : base(message) { }

        protected BaseBusinessException(string message, Exception inner)
            : base(message, inner) { }

        protected BaseBusinessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
