namespace FinLib.Common.Exceptions.Business.User
{
    /// <summary>
    /// خطا مرتبط با مشکلات نقش-سِمَت کاربر
    /// </summary>
    [Serializable]
    public class InvalidUserRoleException : UserRelatedException
    {
        public InvalidUserRoleException(int subjectUserId)
            :base(subjectUserId)
        { }

        public InvalidUserRoleException(int subjectUserId, string message)
            : base(subjectUserId, message) { }

        public InvalidUserRoleException(int subjectUserId, string message, Exception inner)
            : base(subjectUserId, message, inner) { }

        protected InvalidUserRoleException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
