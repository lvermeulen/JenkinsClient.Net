using System;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using HtmlAgilityPack;
using JenkinsClient.Net.Common;
using JenkinsClient.Net.Common.Authentication;
using JenkinsClient.Net.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JenkinsClient.Net
{
	public partial class JenkinsClient
	{
		private static readonly ISerializer s_serializer = new NewtonsoftJsonSerializer(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
		private static readonly HttpContent s_emptyHttpContent = new StringContent("");

		private readonly Url _url;
		private readonly AuthenticationMethod _auth;

		public SecurityCrumb SecurityCrumb { get; set; }

		private readonly Func<HttpCall, Task> _errorHandlerAsync = async call =>
		{
			if (call?.Response == null || !call.Completed)
			{
				return;
			}

			string body = await call.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
			var html = new HtmlDocument();
			html.LoadHtml(body);
			string error = html.DocumentNode
				.SelectSingleNode("/html/head/title")
				.InnerText;

			throw new InvalidOperationException(error);
		};

		public JenkinsClient(string url)
		{
			FlurlHttp.GlobalSettings.OnErrorAsync = _errorHandlerAsync;

			_url = url;
		}

		public IFlurlRequest GetBaseUrl(string url)
		{
			return new Url(url)
				.ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
				.WithAuthentication(_auth)
				.IncludeSecurityCrumb(this);
		}

		public IFlurlRequest GetBaseUrl() => GetBaseUrl(_url);

		public IFlurlRequest GetBaseUrl(string url, string path) => GetBaseUrl(url).AppendPathSegment(path);

		private async Task<TResult> ReadResponseContentAsync<TResult>(HttpResponseMessage responseMessage, Func<string, TResult> contentHandler = null)
		{
			string content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
			return contentHandler != null
				? contentHandler(content)
				: JsonConvert.DeserializeObject<TResult>(content);
		}

		private async Task<bool> ReadResponseContentAsync(HttpResponseMessage responseMessage)
		{
			string content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
			return content.Length == 0;
		}

		private async Task<TResult> HandleResponseAsync<TResult>(HttpResponseMessage responseMessage, Func<string, TResult> contentHandler = null)
		{
			return await ReadResponseContentAsync(responseMessage, contentHandler).ConfigureAwait(false);
		}

		private async Task<bool> HandleResponseAsync(HttpResponseMessage responseMessage)
		{
			return await ReadResponseContentAsync(responseMessage).ConfigureAwait(false);
		}
	}
}
