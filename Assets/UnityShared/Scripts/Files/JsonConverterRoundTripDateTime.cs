using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace UnityShared.Files
{
    /// <summary>
    /// Custom converter to serialize and deserialize Datetime structures in RoundTrip-formatted json text
    /// Info: https://msdn.microsoft.com/en-us/library/az4se3k1%28v=vs.110%29.aspx#Roundtrip
    /// </summary>
    public class JsonConverterRoundTripDateTime : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTime.Parse(reader.Value.ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString("o"));
        }
    }
}
