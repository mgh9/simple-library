using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Business
{

    [Serializable]
    public class FileUploadFailedException : BaseBusinessException
    {
        public FileUploadFailedException() { }
        public FileUploadFailedException(string message) : base(message) { }
        public FileUploadFailedException(string message, Exception inner) : base(message, inner) { }
        protected FileUploadFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}