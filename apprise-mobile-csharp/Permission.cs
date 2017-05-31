using MongoDB.Bson;
using Newtonsoft.Json;
using AppriseMobile.JsonConverters;

namespace AppriseMobile
{
    public class Permission
    {
		[JsonProperty("type")]
        public PermissionType Type;

		[JsonProperty("user")]
		[JsonConverter(typeof(MongoObjectIdConverter))]
		public ObjectId User;
    }
}
