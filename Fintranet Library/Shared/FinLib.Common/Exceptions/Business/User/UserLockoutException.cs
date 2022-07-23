using System;

namespace FinLib.Common.Exceptions.Business.User
{
    /// <summary>
    /// خطا در قفل کردن حساب کاربری رخ داده است
    /// </summary>
    [Serializable]
    public class UserLockoutException : UserRelatedException
    {
        public UserLockoutException(int subjectUserId)
            :base(subjectUserId)
        { }

        public UserLockoutException(int subjectUserId, string message)
            : base(subjectUserId, message) { }

        public UserLockoutException(int subjectUserId, string message, Exception inner)
            : base(subjectUserId, message, inner) { }

        protected UserLockoutException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
