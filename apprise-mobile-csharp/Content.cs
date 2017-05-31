using System;
using System.Collections.Generic;
using System.Net.Http;
using MongoDB.Bson;
using Newtonsoft.Json;
using AppriseMobile.JsonConverters;

namespace AppriseMobile
{
    public class Content
    {
		[JsonProperty("_id")]
        [JsonConverter(typeof(MongoObjectIdConverter))]
        public ObjectId? Id { get; private set; }

		/// <summary>
		/// Title of the content
		/// </summary>
		[JsonProperty("title")]
        public string Title { get; set; }

		/// <summary>
		/// File to upload as the content
		/// </summary>
		[JsonProperty("file")]
        public string File { get; private set; }

		/// <summary>
		/// The web url if the content will be a live link
		/// </summary>
		[JsonProperty("webUrl")]
		public string WebUrl { get; private set; }

		/// <summary>
		/// The id of the folder this content will be contained in
		/// </summary>
		[JsonProperty("contentFolder")]
		[JsonConverter(typeof(MongoObjectIdConverter))]
		public ObjectId? ContentFolder { get; set; }

		/// <summary>
		/// Date the content was originally published
		/// </summary>
		[JsonProperty("publishDate")]
		public DateTime? PublishDate { get; set; }

		/// <summary>
		/// When should this content post to the app. By default it will post immediately. Or it can be set in the future time
		/// </summary>
		[JsonProperty("postDate")]
		public DateTime? PostDate { get; set; }

		/// <summary>
		/// When should this content be auto removed from the app. By default it will not be removed
		/// </summary>
		[JsonProperty("unpostDate")]
		public DateTime? UnpostDate { get; set; }

		/// <summary>
		/// What type of sharing will this content have
		/// </summary>
		[JsonProperty("share")]
		public ContentShare Share { get; set; }

		/// <summary>
		/// Should notes be enabled. this only applies to pdf files
		/// </summary>
		[JsonProperty("notesEnabled")]
		public bool NotesEnabled { get; set; }

		/// <summary>
		/// Should a push notification be sent. by default a push notification is not sent
		/// </summary>
		[JsonProperty("notify")]
		public bool Notify { get; set; }

		/// <summary>
		/// If enabled users must enter their email address before viewing this content
		/// </summary>
		[JsonProperty("secure")]
		public bool Secure { get; set; }

		/// <summary>
		/// If enabled the content will be displayed in the news feed
		/// </summary>
		[JsonProperty("includeInFeed")]
		public bool IncludeInFeed { get; set; }

		/// <summary>
		/// If enabled the content will display a thumbnail
		/// </summary>
		[JsonProperty("displayThumbnail")]
		public bool DisplayThumbnail { get; set; }

		/// <summary>
		/// Optional tag to describe what external system the content is integrated with
		/// </summary>
		[JsonProperty("integrationType")]
		public string IntegrationType { get; set; }

		/// <summary>
		/// Optional id to tie the content back to an external system
		/// </summary>
		[JsonProperty("integrationId")]
		public string IntegrationId { get; set; }

		/// <summary>
		/// The list of groups tagged to this content
		/// </summary>
		[JsonProperty("accessGroups", ItemConverterType = typeof(MongoObjectIdConverter))]
		public List<ObjectId?> AccessGroups { get; private set; }

		[JsonProperty("owner")]
		[JsonConverter(typeof(MongoObjectIdConverter))]
		public ObjectId? Owner { get; private set; }

		[JsonProperty("s3Key")]
		public string S3Key { get; private set; }

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

		[JsonProperty("cacheable")]
		public bool Cacheable { get; private set; }

		[JsonProperty("contentType")]
		public ContentType ContentType { get; private set; }

		[JsonProperty("totalLikes")]
		public long TotalLikes { get; private set; }

		[JsonProperty("totalComments")]
		public long TotalComments { get; private set; }

		[JsonProperty("totalViews")]
		public long TotalViews { get; private set; }

		[JsonProperty("permissions")]
		public List<Permission> Permissions { get; private set; }

		[JsonProperty("hlsVideoPlaylistUrl")]
		public string HlsVideoPlaylistUrl { get; private set; }

		[JsonProperty("transcodeVideoStatus")]
		public JobStatus TranscodeVideoStatus { get; private set; }

		[JsonProperty("thumbnailS3Key")]
		public string ThumbnailS3Key { get; private set; }

		[JsonProperty("smallThumbnailUrl")]
		public string SmallThumbnailUrl { get; private set; }

		[JsonProperty("mediumThumbnailUrl")]
		public string MediumThumbnailUrl { get; private set; }

		[JsonProperty("largeThumbnailUrl")]
		public string LargeThumbnailUrl { get; private set; }

		[JsonProperty("xlargeThumbnailUrl")]
		public string XLargeThumbnailUrl { get; private set; }

		[JsonProperty("thumbnailJobId")]
		public string ThumbnailJobId { get; private set; }

		[JsonProperty("thumbnailJobStatus")]
		public JobStatus ThumbnailJobStatus { get; private set; }

		private Content()
		{
			AccessGroups = new List<ObjectId?>();
			Permissions = new List<Permission>();
		}

		/// <summary>
		/// Create a ContentUploadDescription from a web url
		/// </summary>
		/// <param name="title">Title of the content</param>
		/// <param name="url">The web url</param>
		/// <param name="contentFolder">The id of the folder this content will be contained in</param>
		/// <returns>A new ContentUploadDescription</returns>
		public static Content FromUrl(string title, string url, ObjectId contentFolder)
		{
			return new Content()
			{
				Title = title,
				WebUrl = url,
				ContentFolder = contentFolder
			};
		}

		/// <summary>
		/// Create a Content from a file path
		/// </summary>
		/// <param name="title">Title of the content</param>
		/// <param name="url">The file to upload as content</param>
		/// <param name="contentFolder">The id of the folder this content will be contained in</param>
		/// <returns>A new Content</returns>
		public static Content FromFile(string title, string file, ObjectId contentFolder)
		{
			return new Content()
			{
				Title = title,
				File = file,
				ContentFolder = contentFolder
			};
		}
    }
}
