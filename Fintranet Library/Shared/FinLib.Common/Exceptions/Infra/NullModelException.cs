using System;

namespace FinLib.Common.Exceptions.Infra
{
    /// <summary>
    /// مدل نال است
    /// برای مثال در متدها، هنگامی که میخاهیم مدل رو بررسی کنیم و باهاش کار کنیم، اگه
    /// نال باشه، همچین اکسپشنی رو میشه صادر کرد
    /// </summary>
    [Serializable]
    public class NullModelException : InvalidModelException
    {
        public NullModelException(string modelName, string message = null)
            : base(message ?? $"Model '{modelName}' cannot be null") { }

        public NullModelException(string modelName, string message, Exception inner)
            : base(message ?? $"Model '{modelName}' cannot be null", inner) { }

        protected NullModelException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
