using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Business
{
    [Serializable]
    public class GeneralBusinessLogicException : BaseBusinessException
    {
        public GeneralBusinessLogicException() { }

        public GeneralBusinessLogicException(string message)
            : base(message) { }

        public GeneralBusinessLogicException(string message, Exception inner)
            : base(message, inner) { }

        protected GeneralBusinessLogicException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
