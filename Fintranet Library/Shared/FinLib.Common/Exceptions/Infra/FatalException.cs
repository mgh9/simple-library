using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Infra
{
    /// <summary>
    /// برای زمانی که عملکری ممکن است باعث اختلال شدید و خطای حاد، در سیستم گردد یا گردیده است
    /// </summary>
    [Serializable]
    public class FatalException : BaseInfraException
    {
        public FatalException() { }

        public FatalException(string message)
            : base(message) { }

        public FatalException(string message, Exception inner)
            : base(message, inner) { }

        protected FatalException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
