using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Business
{
    [Serializable]
    public class ConcurrentLoginNotAllowedException : BaseBusinessException
    {
        public ConcurrentLoginNotAllowedException() { }
        public ConcurrentLoginNotAllowedException(string message) : base(message) { }
        public ConcurrentLoginNotAllowedException(string message, Exception inner) : base(message, inner) { }
        protected ConcurrentLoginNotAllowedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
