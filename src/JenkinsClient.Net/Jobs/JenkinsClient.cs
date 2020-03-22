using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using JenkinsClient.Net.Models;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net
{
	public partial class JenkinsClient
	{
		public IFlurlRequest GetJobsUrl() => GetBaseUrl("api/json");

		public IFlurlRequest GetJobsUrl(string path) => GetJobsUrl().AppendPathSegment(path);

		public async Task<IEnumerable<Job>> GetJobsAsync()
		{
			var systemInformation = await GetSystemInformationAsync().ConfigureAwait(false);
			return systemInformation.Jobs;
		}

		public async Task<IEnumerable<JobInformation>> GetJobInformationsAsync()
		{
			var results = new List<JobInformation>();

			var jobs = await GetJobsAsync().ConfigureAwait(false);
			foreach (var job in jobs)
			{
				results.Add(await GetJobInformationAsync(job.Url).ConfigureAwait(false));
			}

			return results;
		}

		public async Task<JobInformation> GetJobInformationAsync(string jobUrl)
		{
			return await jobUrl
				.AppendPathSegment("api/json")
				.GetJsonAsync<JobInformation>()
				.ConfigureAwait(false);
		}
	}
}
