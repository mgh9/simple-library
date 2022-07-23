using FinLib.Common.Exceptions.Base;
using System;

namespace FinLib.Common.Exceptions.Infra
{
    /// <summary>
    /// خطا در انجام عملیات مرتب سازی رخ داده است
    /// </summary>
    [Serializable]
    public class InvalidSortException : BaseException
    {
        public InvalidSortException() { }

        public InvalidSortException(string message) : base(message) { }

        public InvalidSortException(string message, Exception inner) : base(message, inner) { }

        protected InvalidSortException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}