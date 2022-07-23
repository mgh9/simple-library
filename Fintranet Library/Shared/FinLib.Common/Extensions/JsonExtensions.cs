using Newtonsoft.Json;
using FinLib.Common.Helpers.Json;

namespace FinLib.Common.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object value)
        {
            return JsonHelper.ToJson(value, PreserveReferencesHandling.None);
        }

        public static string ToJson(this object value, PreserveReferencesHandling preserveReferencesHandling)
        {
            return JsonHelper.ToJson(value, preserveReferencesHandling);
        }
    }
}
