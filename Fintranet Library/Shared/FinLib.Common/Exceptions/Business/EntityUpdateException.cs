using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Business
{
    /// <summary>
    /// خطا در هنگام ویرایش یک موجودیت رخ داده است
    /// </summary>
    [Serializable]
    public class EntityUpdateException : BaseBusinessException
    {
        public EntityUpdateException(int entityId) 
        {
            EntityId = entityId;
        }

        public EntityUpdateException(int entityId, string message)
            : base(message) 
        {
            EntityId = entityId;
        }

        public EntityUpdateException(int entityId, string message, Exception inner)
            : base(message, inner) 
        {
            EntityId = entityId;
        }

        protected EntityUpdateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public int EntityId { get; set; }        
    }
}
