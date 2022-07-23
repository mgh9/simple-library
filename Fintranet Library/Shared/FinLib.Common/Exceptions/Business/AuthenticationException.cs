using FinLib.Common.Exceptions.Base;

namespace FinLib.Common.Exceptions.Business
{
    [System.Serializable]
    public class AuthenticationException : BaseBusinessException
    {
        public AuthenticationException(string message) : base(message)
        {
        }

        public AuthenticationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public AuthenticationException()
        {
        }

        protected AuthenticationException(System.Runtime.Serialization.SerializationInfo info
            , System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
