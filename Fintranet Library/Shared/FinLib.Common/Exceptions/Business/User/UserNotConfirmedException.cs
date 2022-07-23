using System;

namespace FinLib.Common.Exceptions.Business.User
{

    [Serializable]
    public class UserNotConfirmedException : UserRelatedException
    {
        public UserNotConfirmedException(int subjectUserId)
            :base(subjectUserId)
        {
        }

        public UserNotConfirmedException(int subjectUserId, string message)
            : base(subjectUserId, message)
        {
        }

        public UserNotConfirmedException( int subjectUserId, string message, Exception inner) : base(subjectUserId, message, inner) { }

        protected UserNotConfirmedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
