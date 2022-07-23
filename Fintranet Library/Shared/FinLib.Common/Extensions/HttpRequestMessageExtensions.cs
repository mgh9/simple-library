using FinLib.Common.Exceptions.Infra;
using System;
using System.Net.Http;

namespace FinLib.Common.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        private const string LogId = "LOG_ID";

        public static void SetLogId(this HttpRequestMessage request, Guid id)
        {
            request.ThrowIfNull();

            request.Options.Set(new HttpRequestOptionsKey<Guid>(LogId), id);
            //request.Properties[LogId] = id;
        }

        public static Guid GetLogId(this HttpRequestMessage request)
        {
            request.ThrowIfNull();

            if (request.Options.TryGetValue<Guid>(new HttpRequestOptionsKey<Guid>(LogId), out Guid value))
            //if (request.Properties.TryGetValue(LogId, out object value))
            {
                return value;
            }

            return Guid.Empty;
        }
    }

}
