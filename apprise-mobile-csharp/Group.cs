using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Newtonsoft.Json;
using AppriseMobile.JsonConverters;

namespace AppriseMobile
{
    public class Group
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

        [JsonProperty("created")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? Created { get; private set; }

        [JsonProperty("modified")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? Modified { get; private set; }

        [JsonProperty("modifiedBy")]
        [JsonConverter(typeof(MongoObjectIdConverter))]
        public ObjectId? ModifiedBy { get; private set; }

        [JsonProperty("contentFeedProfiles", ItemConverterType = typeof(MongoObjectIdConverter))]
        public List<ObjectId?> ContentFeedProfiles { get; private set; }

        [JsonProperty("ManageType")]
        public ManageType ManageType { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("permissions")]
        public List<Permission> Permissions { get; private set; }

        [JsonProperty("integrationId")]
        public string IntegrationId { get; private set; }

        [JsonProperty("integrationType")]
        public string IntegrationType { get; private set; }

        [JsonProperty("jobId")]
        public string JobId { get; private set; }

        [JsonProperty("jobStatus")]
        public JobStatus JobStatus { get; private set; }

		public Group(string name)
		{
			Name = name;
			ContentFeedProfiles = new List<ObjectId?>();
			Permissions = new List<Permission>();
		}
    }
}
