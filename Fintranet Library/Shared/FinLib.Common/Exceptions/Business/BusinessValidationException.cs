using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Business
{
    /// <summary>
    /// نقض قوانین اعتبارسنجی
    /// </summary>
    [Serializable]
    public class BusinessValidationException : BaseBusinessException
    {
        public BusinessValidationException() { }

        public BusinessValidationException(string message) : base(message) { }

        public BusinessValidationException(string message, Exception inner) : base(message, inner) { }

        protected BusinessValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
