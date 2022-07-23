using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Infra
{
    /// <summary>
    /// پیاده سازی یک عملکرد در برنامه، اشتباه است یا ناقص
    /// </summary>
    [Serializable]
    public class InvalidImplementationException : BaseInfraException
    {
        public InvalidImplementationException() { }

        public InvalidImplementationException(string message)
            : base(message) { }

        public InvalidImplementationException(string message, Exception inner)
            : base(message, inner) { }

        protected InvalidImplementationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
