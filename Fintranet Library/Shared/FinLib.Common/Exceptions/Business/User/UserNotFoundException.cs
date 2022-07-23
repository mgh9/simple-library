using System;

namespace FinLib.Common.Exceptions.Business.User
{

    [Serializable]
    public class UserNotFoundException : UserRelatedException
    {
        public UserNotFoundException(string subjectUserName)
            : base(0)
        {
            SubjectUserName = subjectUserName;
        }

        public UserNotFoundException(string subjectUserName, string message)
            : base(0, message)
        {
            SubjectUserName = subjectUserName;

        }

        public UserNotFoundException(string subjectUserName, string message, Exception inner)
            : base(0, message, inner)
        {
            SubjectUserName = subjectUserName;
        }

        protected UserNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
