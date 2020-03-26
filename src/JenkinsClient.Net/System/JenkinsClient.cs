using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using JenkinsClient.Net.Models;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net
{
	public partial class JenkinsClient
	{
		public IFlurlRequest GetSystemUrl() => GetBaseUrl();

		public IFlurlRequest GetSystemApiUrl() => GetSystemUrl().AppendPathSegment("api/json");

		public IFlurlRequest GetSystemApiUrl(string path) => GetSystemUrl().AppendPathSegments(path, "api/json");

		public async Task<SystemInformation> GetSystemInformationAsync()
		{
			return await GetSystemApiUrl()
				.GetJsonAsync<SystemInformation>()
				.ConfigureAwait(false);
		}

		public async Task<string> GetVersionAsync()
		{
			var response = await GetSystemApiUrl()
				.GetAsync(completionOption: HttpCompletionOption.ResponseHeadersRead)
				.ConfigureAwait(false);

			if (response.Headers.TryGetValues("X-Jenkins", out var values))
			{
				return values.FirstOrDefault();
			}

			return null;
		}

		public async Task<SecurityCrumb> GetSecurityCrumbAsync()
		{
			return await GetSystemApiUrl("crumbIssuer")
				.GetJsonAsync<SecurityCrumb>()
				.ConfigureAwait(false);
		}

		public async Task SetSecurityCrumbAsync()
		{
			SecurityCrumb = await GetSecurityCrumbAsync().ConfigureAwait(false);
		}

		public async Task<bool> QuietDownAsync()
		{
			var response = await GetSystemUrl()
				.AppendPathSegment("quietDown")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}

		public async Task<bool> CancelQuietDownAsync()
		{
			var response = await GetSystemUrl()
				.AppendPathSegment("cancelQuietDown")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}

		public async Task<bool> RestartAsync()
		{
			var response = await GetSystemUrl()
				.AppendPathSegment("restart")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}

		public async Task<bool> SafeRestartAsync()
		{
			var response = await GetSystemUrl()
				.AppendPathSegment("safeRestart")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}
	}
}
