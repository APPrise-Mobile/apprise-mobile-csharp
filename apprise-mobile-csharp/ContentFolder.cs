using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Newtonsoft.Json;
using AppriseMobile.JsonConverters;

namespace AppriseMobile
{
    public class ContentFolder
    {
		[JsonProperty("_id")]
		[JsonConverter(typeof(MongoObjectIdConverter))]
        public ObjectId? Id { get; private set; }

		[JsonProperty("owner")]
		[JsonConverter(typeof(MongoObjectIdConverter))]
		public ObjectId? Owner { get; private set; }

		[JsonProperty("account")]
		[JsonConverter(typeof(MongoObjectIdConverter))]
		public ObjectId? Account { get; private set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("created")]
		[JsonConverterAttribute(typeof(IsoDateTimeConverter))]
		public DateTime? Created { get; private set; }

		[JsonProperty("modified")]
		[JsonConverterAttribute(typeof(IsoDateTimeConverter))]
		public DateTime? Modified { get; private set; }

		[JsonProperty("modifiedBy")]
		[JsonConverter(typeof(MongoObjectIdConverter))]
		public ObjectId? ModifiedBy { get; private set; }

		[JsonProperty("parent")]
		[JsonConverter(typeof(MongoObjectIdConverter))]
		public ObjectId? Parent { get; set; }

		[JsonProperty("children", ItemConverterType = typeof(MongoObjectIdConverter))]
		public List<ObjectId?> Children { get; private set; }

		[JsonProperty("integrationId")]
		public string IntegrationId { get; set; }

		[JsonProperty("integrationType")]
		public string IntegrationType { get; set; }

		[JsonProperty("permissions")]
		public List<Permission> Permissions { get; private set; }

		public ContentFolder(string title)
		{
			Title = title;
			Permissions = new List<Permission>();
			Children = new List<ObjectId?>();
		}
    }
}
