using System;

namespace FinLib.Common.Exceptions.Infra
{
    /// <summary>
    /// کلید اصلی موجودیت نامعتبر است.
    /// برای مثال در متدها، هنگامی که میخاهیم اپدیت کنیم یا فایند کنیم موجودیتی رو، اگه کلید اصلی صفر یا منفی باشه، این اکسپشن رو میشه صادر کرد
    /// </summary>
    [Serializable]
    public class InvalidPrimaryKeyException : InvalidModelException
    {
        public InvalidPrimaryKeyException(string primaryKeyName)
            : base($"{primaryKeyName} should be numeric and > 1") { }

        public InvalidPrimaryKeyException(string message, Exception inner)
            : base(message, inner) { }

        protected InvalidPrimaryKeyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
