using System;

namespace FinLib.Common.Exceptions.Infra
{
    /// <summary>
    /// در شرایطی که انتظار میرود از موجودیت مورد نظر، تنها
    /// یک نسخه وجود داشته باشد ولی بیش از یک نسخه یافت شئد. یعنی سامانه باید این تضمین رو میداده اما
    /// الان به دلیل، این نقض شده
    /// </summary>
    [Serializable]
    public class DuplicateEntityFoundException : FatalException
    {
        public DuplicateEntityFoundException() { }

        public DuplicateEntityFoundException(string message) : base(message) { }

        public DuplicateEntityFoundException(string message, Exception inner) : base(message, inner) { }

        protected DuplicateEntityFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
