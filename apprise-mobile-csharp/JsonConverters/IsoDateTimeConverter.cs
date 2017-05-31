using System;
using System.Globalization;
using Newtonsoft.Json;

namespace AppriseMobile.JsonConverters
{
    public class IsoDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(DateTime?).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
			var value = reader.Value?.ToString();
			if (value == null || value == string.Empty)
			{
				if (objectType == typeof(DateTime?)) return null;
				throw new Exception("DateTime cannot be null or empty");
			}
            return DateTime.Parse(value, null, DateTimeStyles.RoundtripKind);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
			var dateTime = value as DateTime?;
			if (dateTime == null) writer.WriteValue((string)null);
            else writer.WriteValue((dateTime.Value.ToUniversalISO()));
        }
    }
}
