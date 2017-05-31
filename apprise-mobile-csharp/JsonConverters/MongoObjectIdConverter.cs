using System;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AppriseMobile.JsonConverters
{
    public class MongoObjectIdConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
			return typeof(ObjectId?).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
			var value = reader.Value?.ToString();
			if (value == null || value == string.Empty)
			{
				if (objectType == typeof(ObjectId?)) return null;
				return ObjectId.Empty;
			}
			return new ObjectId(value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
			var id = value as ObjectId?;
			if (id == null || id == ObjectId.Empty) writer.WriteValue((string)null);
			else writer.WriteValue(id.ToString());
        }
    }
}
