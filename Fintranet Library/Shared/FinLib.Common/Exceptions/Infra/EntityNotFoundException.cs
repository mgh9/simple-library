using System;

namespace FinLib.Common.Exceptions.Infra
{
    /// <summary>
    /// در شرایطی که شناسه موجودیت انتظار می رود معتبر باشد و یافت شود، اما یافت نمی شود
    /// این یک خطای بحرانی می تواند تلقی شود
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : FatalException
    {
        public EntityNotFoundException(int requestingEntityId)
        {
            RequestingEntityId = requestingEntityId;
        }

        public EntityNotFoundException(int requestingEntityId, string message)
            : base(message)
        {
            RequestingEntityId = requestingEntityId;
        }

        public EntityNotFoundException(string requestingByThisValue, string message)
            : base(message)
        {
            RequestingByThisValue = requestingByThisValue;
        }

        public EntityNotFoundException(int requestingEntityId, string message, Exception inner)
            : base(message, inner)
        {
            RequestingEntityId = requestingEntityId;
        }

        public EntityNotFoundException(string requestingByThisValue, string message, Exception inner)
            : base(message, inner)
        {
            RequestingByThisValue = requestingByThisValue;
        }

        protected EntityNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public int RequestingEntityId { get; }
        public string RequestingByThisValue { get; }

        public override string ToString()
        {
            return $"موجودیت با شناسه ی '{RequestingByThisValue}' یافت نشد";
        }
    }
}
