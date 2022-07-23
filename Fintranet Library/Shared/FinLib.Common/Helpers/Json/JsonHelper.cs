using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FinLib.Common.Helpers.Json
{
    public static class JsonHelper
    {
        public static JsonSerializerSettings JsonSerializerSettings
        {
            get
            {
                var result = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                return result;
            }
        }

        public static string ToJson(object value)
        {
            return ToJson(value, PreserveReferencesHandling.None);
        }

        public static string ToJson(object value , PreserveReferencesHandling preserveReferencesHandling)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = preserveReferencesHandling,
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                MaxDepth = 6
            };

            return JsonConvert.SerializeObject(value, settings);
        }
    }
}
