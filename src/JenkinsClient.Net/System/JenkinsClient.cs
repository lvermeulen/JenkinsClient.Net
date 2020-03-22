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

		public IFlurlRequest GetSystemUrl(string path) => GetSystemUrl().AppendPathSegment(path);

		public async Task<SystemInformation> GetSystemInformationAsync()
		{
			return await GetJobsUrl()
				.GetJsonAsync<SystemInformation>()
				.ConfigureAwait(false);
		}

		public async Task<string> GetVersionAsync()
		{
			var response = await GetBaseUrl("/")
				.GetAsync()
				.ConfigureAwait(false);

			if (response.Headers.TryGetValues("X-Jenkins", out var values))
			{
				return values.FirstOrDefault();
			}

			return null;
		}
	}
}
