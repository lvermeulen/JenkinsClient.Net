using System;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using JenkinsClient.Net.Common;
using JenkinsClient.Net.Common.Authentication;
using JenkinsClient.Net.Common.Models;
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
			var sonarQubeErrors = call.FlurlRequest.Settings.JsonSerializer.Deserialize<JenkinsError>(body);
			var exception = new InvalidOperationException(sonarQubeErrors.Error);

			throw exception;
		};

		public JenkinsClient(string url)
		{
			FlurlHttp.GlobalSettings.OnErrorAsync = _errorHandlerAsync;

			_url = url;
		}

		public IFlurlRequest GetBaseUrl()
		{
			return new Url(_url)
				.ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
				.WithAuthentication(_auth)
				.IncludeSecurityCrumb(this);
		}

		public IFlurlRequest GetBaseUrl(string path) => GetBaseUrl().AppendPathSegment(path);

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

		private async Task HandleErrorsAsync(HttpResponseMessage response)
		{
			if (!response.IsSuccessStatusCode)
			{
				var jenkinsError = await ReadResponseContentAsync<JenkinsError>(response).ConfigureAwait(false);

				// ReSharper disable once AssignNullToNotNullAttribute
				string errorMessage = jenkinsError.Error;
				throw new InvalidOperationException($"Http request failed ({(int)response.StatusCode} - {response.StatusCode}):\n{errorMessage}");
			}
		}

		private async Task<TResult> HandleResponseAsync<TResult>(HttpResponseMessage responseMessage, Func<string, TResult> contentHandler = null)
		{
			await HandleErrorsAsync(responseMessage).ConfigureAwait(false);
			return await ReadResponseContentAsync(responseMessage, contentHandler).ConfigureAwait(false);
		}

		private async Task<bool> HandleResponseAsync(HttpResponseMessage responseMessage)
		{
			await HandleErrorsAsync(responseMessage).ConfigureAwait(false);
			return await ReadResponseContentAsync(responseMessage).ConfigureAwait(false);
		}
	}
}
