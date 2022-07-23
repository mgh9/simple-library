using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Business
{
    /// <summary>
    /// خطا در هنگام حذف یک موجودیت رخ داده است
    /// </summary>
    [Serializable]
    public class EntityDeleteException : BaseBusinessException
    {
        public EntityDeleteException(int entityId) 
        {
            EntityId = entityId;
        }

        public EntityDeleteException(int entityId, string message)
            : base(message) 
        {
            EntityId = entityId;
        }

        public EntityDeleteException(int entityId, string message, Exception inner)
            : base(message, inner) 
        {
            EntityId = entityId;
        }

        protected EntityDeleteException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public int EntityId { get; set; }
    }
}
