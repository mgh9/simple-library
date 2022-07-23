using System;

namespace FinLib.Common.Exceptions.Business.User
{
    /// <summary>
    /// خطا در باز کردن حساب کاربری رخ داده است
    /// </summary>
    [Serializable]
    public class UserUnlockException : UserRelatedException
    {
        public UserUnlockException(int subjectUserId)
            :base(subjectUserId)
        { }

        public UserUnlockException(int subjectUserId, string message)
            : base(subjectUserId, message) { }

        public UserUnlockException(int subjectUserId, string message, Exception inner)
            : base(subjectUserId, message, inner) { }

        protected UserUnlockException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
