using System;

namespace FinLib.Common.Exceptions.Infra
{
    /// <summary>
    /// رکورد تنظیماتی که باید حتما وجود داشته باشه، یافت نشد
    /// </summary>
    [Serializable]
    public class ConfigNotFoundException : Base.BaseInfraException
    {
        public ConfigNotFoundException() { }

        public ConfigNotFoundException(string message) : base(message) { }

        public ConfigNotFoundException(string message, Exception inner) : base(message, inner) { }

        protected ConfigNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
