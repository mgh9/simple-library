using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Business
{
    /// <summary>
    /// خطا در هنگام ایجاد یک موجودیت رخ داده است
    /// </summary>
    [Serializable]
    public class EntityCreateException : BaseBusinessException
    {
        public EntityCreateException() { }

        public EntityCreateException(string message)
            : base(message) { }

        public EntityCreateException(string message, Exception inner)
            : base(message, inner) { }

        protected EntityCreateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
