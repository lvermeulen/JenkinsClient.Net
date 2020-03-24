using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using JenkinsClient.Net.Models;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net
{
	public partial class JenkinsClient
	{
		public IFlurlRequest GetSystemUrl() => GetBaseUrl("api/json");

		public async Task<SystemInformation> GetSystemInformationAsync()
		{
			return await GetSystemUrl()
				.GetJsonAsync<SystemInformation>()
				.ConfigureAwait(false);
		}

		public async Task<string> GetVersionAsync()
		{
			var response = await GetBaseUrl()
				.GetAsync()
				.ConfigureAwait(false);

			if (response.Headers.TryGetValues("X-Jenkins", out var values))
			{
				return values.FirstOrDefault();
			}

			return null;
		}

		public async Task<SecurityCrumb> GetSecurityCrumbAsync()
		{
			return await GetBaseUrl()
				.AppendPathSegment("crumbIssuer/api/json")
				.GetJsonAsync<SecurityCrumb>()
				.ConfigureAwait(false);
		}

		public async Task SetSecurityCrumbAsync()
		{
			SecurityCrumb = await GetSecurityCrumbAsync().ConfigureAwait(false);
		}

		public async Task<bool> QuietDownAsync()
		{
			var response = await GetBaseUrl("quietDown")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}

		public async Task<bool> CancelQuietDownAsync()
		{
			var response = await GetBaseUrl("cancelQuietDown")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}

		public async Task<bool> RestartAsync()
		{
			var response = await GetBaseUrl("restart")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}

		public async Task<bool> SafeRestartAsync()
		{
			var response = await GetBaseUrl("safeRestart")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}
	}
}
