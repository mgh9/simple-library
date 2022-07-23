using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Infra
{
    /// <summary>
    /// مدل نامعتبر است
    /// برای مثال در متدها، هنگامی که میخاهیم مدل رو بررسی کنیم و باهاش کار کنیم، اگه
    /// نامعتبر باشه از لحاظ غیر بیزینسی (مثلا نال باشه، نه اینکه مثلا یک مقدار داخلش بیزینس رول رو نقض کنه)، همچین اکسپشنی رو میشه
    /// صادر کرد
    /// </summary>
    [Serializable]
    public class InvalidModelException : BaseInfraException
    {
        public InvalidModelException(string message)
            : base(message) { }

        public InvalidModelException(string message, Exception inner)
            : base(message, inner) { }

        protected InvalidModelException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
