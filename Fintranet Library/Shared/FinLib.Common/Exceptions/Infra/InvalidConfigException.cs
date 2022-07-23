using System;

namespace FinLib.Common.Exceptions.Infra
{
    /// <summary>
    /// رکورد تنظیماتی نامعتبر می باشد
    /// </summary>
    [Serializable]
    public class InvalidConfigException : Base.BaseInfraException
    {
        public InvalidConfigException() { }

        public InvalidConfigException(string message) : base(message) { }

        public InvalidConfigException(string message, Exception inner) : base(message, inner) { }

        protected InvalidConfigException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
