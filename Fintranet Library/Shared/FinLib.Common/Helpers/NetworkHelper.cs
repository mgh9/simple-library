using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using FinLib.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinLib.Common.Helpers
{
    public class NetworkHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NetworkHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetMachineNameFromIPAddress(string ipAdress)
        {
            try
            {
                System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(ipAdress);

                string machineName = hostEntry.HostName;
                return machineName;
            }
            catch (Exception ex)
            {
                return "NA:" + ex.Message;
            }
        }

        public string GetRequestIP(bool tryUseXForwardHeader = true)
        {
            string ip = null;

            // todo support new "Forwarded" header (2014) https://en.wikipedia.org/wiki/X-Forwarded-For

            // X-Forwarded-For (csv list):  Using the First entry in the list seems to work
            // for 99% of cases however it has been suggested that a better (although tedious)
            // approach might be to read each IP from right to left and use the first public IP.
            // http://stackoverflow.com/a/43554000/538763
            //
            if (tryUseXForwardHeader)
                ip = splitCsv(getHeaderValueAs<string>("X-Forwarded-For")).FirstOrDefault();

            // RemoteIpAddress is always null in DNX RC1 Update1 (bug).
            if (ip.IsEmpty() && _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress != null)
                ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            if (ip.IsEmpty())
                ip = getHeaderValueAs<string>("REMOTE_ADDR");

            // _httpContextAccessor.HttpContext?.Request?.Host this is the local host.

            if (ip.IsEmpty())
                return "NA";
            //throw new Exception("Unable to determine caller's IP.");

            return ip;
        }

        private T getHeaderValueAs<T>(string headerName)
        {
            StringValues values = StringValues.Empty;
            if (_httpContextAccessor.HttpContext?.Request?.Headers?.TryGetValue(headerName, out values) ?? false)
            {
                string rawValues = values.ToString();   // writes out as Csv when there are multiple.

                if (!rawValues.IsEmpty())
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
            }
            return default;
        }

        private static List<string> splitCsv(string csvList, bool nullOrWhitespaceInputReturnsNull = false)
        {
            if (string.IsNullOrWhiteSpace(csvList))
                return nullOrWhitespaceInputReturnsNull ? null : new List<string>();

            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable()
                .Select(s => s.Trim())
                .ToList();
        }
    }
}
