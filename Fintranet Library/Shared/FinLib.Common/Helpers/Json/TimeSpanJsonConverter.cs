using FinLib.Common.Extensions;
using Newtonsoft.Json;

namespace FinLib.Common.Helpers.Json
{
    public class TimeSpanJsonConverter : JsonConverter
    {
        public override bool CanWrite => true;
        public override bool CanRead => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.ThrowIfNull();

            var ts = (TimeSpan)value;
            writer.WriteValue($"{ts.Hours:00}:{ts.Minutes:00}");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(TimeSpan?) == objectType;
        }
    }
}
