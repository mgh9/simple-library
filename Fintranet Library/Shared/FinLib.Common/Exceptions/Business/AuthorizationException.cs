using FinLib.Common.Exceptions.Base;

namespace FinLib.Common.Exceptions.Business
{
    [System.Serializable]
    public class AuthorizationException : BaseBusinessException
    {
        public AuthorizationException(string message) : base(message)
        {
        }

        public AuthorizationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public AuthorizationException()
        {
        }

        protected AuthorizationException(System.Runtime.Serialization.SerializationInfo info
            , System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
