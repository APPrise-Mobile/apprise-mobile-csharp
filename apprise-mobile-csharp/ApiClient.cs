using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AppriseMobile
{
    public class ApiClient : IDisposable
    {
		private readonly string baseUrl;
		private readonly string grantCode;
		private readonly HttpClient httpClient;

		static ApiClient()
		{
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		}

		public ApiClient(ApiEnvironment environment, string grantCode)
		{
			httpClient = new HttpClient();

			this.grantCode = grantCode;

			switch (environment)
			{
				case ApiEnvironment.Beta:
					baseUrl = "https://beta-api.indiciummobile.com/v2";
					break;

				default:
					baseUrl = "https://api.theemployeeapp.com/v2";
					break;
			}
		}

		private string BuildApiUrl(string path, int limit = 0, int offset = 0)
		{
			if (path[0] != '/') path = '/' + path;

			var queryString = "?code=" + grantCode;
			if (limit > 0)
			{
				if (limit < 10) limit = 10;
				queryString += "&limit=" + limit;
			}
			if (offset > 0) queryString += "&offset=" + offset;

			return baseUrl + path + queryString;
		}

		private IEnumerable<T> GetList<T>(string path, int limit = 10, int offset = 0)
		{
			var url = BuildApiUrl(path, limit, offset);

			var response = httpClient.GetAsync(url).Result;
			response.EnsureSuccessStatusCode();

			var content = response.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<List<T>>(content);
		}

		private T Get<T>(string path, ObjectId id)
		{
			var url = BuildApiUrl(path + "/" + id.ToString());

            var response = httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);
		}

        private void UploadFile(string apiPath, string filePath, Dictionary<string,string> otherParams = null)
		{
			var content = new MultipartFormDataContent();

			using (var streamContent = new StreamContent(File.OpenRead(filePath)))
			{
				content.Add(streamContent, "file", Path.GetFileName(filePath));

                if (otherParams != null)
                {
                    foreach(var pair in otherParams)
                    {
                        content.Add(new StringContent(pair.Value), pair.Key);
                    }
                }

				var response = httpClient.PostAsync(BuildApiUrl(apiPath), content).Result;
				response.EnsureSuccessStatusCode();
			}
		}

        /// <summary>
        /// Upload a csv to add directories and directory entries to the platform
        /// </summary>
        /// <param name="filePath">Csv of entries to upload</param>
        /// <param name="directoryName">(Optional) Name of the directory to upload to</param>
        public void UploadDirectoryCsv(string filePath, string directoryName = "")
        {
            if (directoryName == null || directoryName == string.Empty) UploadFile("/directory/account-csv", filePath);
            else
            {
                var otherParams = new Dictionary<string, string>
                {
                    ["directoryName"] = directoryName
                };
                UploadFile("/directory/single-csv", filePath, otherParams);
            }
		}

		/// <summary>
		/// Upload a csv to add users to the platform
		/// </summary>
		/// <param name="filePath">Csv file of users to upload</param>
		/// <remarks>
		/// The file needs to contain two columns.
		/// The first column will be your users' unique identifiers.
		/// The second column will be the name of the group the user is in.
		/// If the user is in multiple groups they can b in multiple rows in the csv
		/// </remarks>
		public void UploadUserCsv(string filePath) => UploadFile("/users/bulk-csv", filePath);

		/// <summary>
		/// Add a new content to the account
		/// </summary>
		/// <param name="content">Content to create</param>
		public void UploadContent(Content content)
		{
			StreamContent fileContent = null;

			try
			{
				var httpContent = new MultipartFormDataContent();

				httpContent.AddString(content.Title, "title");
				httpContent.AddString(content.ContentFolder.ToString(), "contentFolder");
				httpContent.AddString(content.Share.ToEnumString(), "share");
                httpContent.AddString(content.Caption, "caption");

				httpContent.AddBool(content.NotesEnabled, "notesEnabled");
				httpContent.AddBool(content.Notify, "notify");
				httpContent.AddBool(content.Secure, "secure");
				httpContent.AddBool(content.IncludeInFeed, "includeInFeed");
				httpContent.AddBool(content.DisplayThumbnail, "displayThumbnail");

				if (content.PublishDate != null) httpContent.AddString(content.PublishDate.ToUniversalISO(), "publishDate");
				if (content.PostDate != null) httpContent.AddString(content.PostDate.ToUniversalISO(), "postDate");
				if (content.UnpostDate != null) httpContent.AddString(content.UnpostDate.ToUniversalISO(), "unpostDate");

				if (content.IntegrationId != null) httpContent.AddString(content.IntegrationId, "integrationId");
				if (content.IntegrationType != null) httpContent.AddString(content.IntegrationType, "integrationType");

				foreach (var group in content.AccessGroups)
				{
					httpContent.AddString(group.ToString(), "accessGroups");
				}

				if (content.WebUrl != null) httpContent.AddString(content.WebUrl, "webUrl");
				else
				{
					fileContent = new StreamContent(File.OpenRead(content.File));
					httpContent.Add(fileContent, "file", Path.GetFileName(content.File));
				}

				var response = httpClient.PostAsync(BuildApiUrl("/contents"), httpContent).Result;
				response.EnsureSuccessStatusCode();
			}
			finally
			{
				fileContent?.Dispose();
			}
		}

		public IEnumerable<ContentFolder> GetContentFolders(int limit = 10, int offset = 0) => GetList<ContentFolder>("/content-folders", limit, offset);
		public ContentFolder GetContentFolder(ObjectId id) => Get<ContentFolder>("/content-folders", id);

		public IEnumerable<Content> GetContents(int limit = 0, int offset = 0) => GetList<Content>("/contents", limit, offset);
		public Content GetContent(ObjectId id) => Get<Content>("/contents", id);

        public IEnumerable<Group> GetGroups(int limit = 0, int offset = 0) => GetList<Group>("/groups", limit, offset);
        public Group GetGroup(ObjectId id) => Get<Group>("/groups", id);

		public void Dispose()
		{
			httpClient.Dispose();
		}
    }
}
