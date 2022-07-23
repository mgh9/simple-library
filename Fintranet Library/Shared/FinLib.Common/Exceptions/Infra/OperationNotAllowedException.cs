using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Infra
{
    /// <summary>
    /// امکان اجرا این عملیات وجود ندارد
    /// </summary>
    [Serializable]
    public class OperationNotAllowedException : BaseInfraException
    {
        public OperationNotAllowedException() { }

        public OperationNotAllowedException(string message)
            : base(message) { }

        public OperationNotAllowedException(string message, Exception inner)
            : base(message, inner) { }

        protected OperationNotAllowedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
