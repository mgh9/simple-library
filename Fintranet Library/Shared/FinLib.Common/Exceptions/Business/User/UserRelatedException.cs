using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Business.User
{
    /// <summary>
    /// خطاهای بیزینس مرتبط با کاربر
    /// در این جنس از خطا، در اکثر مواقع شناسه کاربر را داریم
    /// </summary>
    [Serializable]
    public abstract class UserRelatedException : BaseBusinessException
    {
        public int SubjectUserId { get; set; }
        public string SubjectUserName { get; set; }

        protected UserRelatedException(int subjectUserId)
        {
            SubjectUserId = subjectUserId;
        }

        protected UserRelatedException(int subjectUserId, string message)
            : base(message)
        {
            SubjectUserId = subjectUserId;
        }

        protected UserRelatedException(int subjectUserId, string message, Exception inner)
            : base(message, inner)
        {
            SubjectUserId = subjectUserId;
        }

        protected UserRelatedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
