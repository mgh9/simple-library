using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Business
{
    [Serializable]
    public class IllegalRequestException : BaseBusinessException
    {
        public IllegalRequestException() : this("دسترسی به این اطلاعات مقدور نمی باشد") { }

        public IllegalRequestException(string message) : base(message) { }

        public IllegalRequestException(string message, Exception inner) : base(message, inner) { }

        protected IllegalRequestException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
