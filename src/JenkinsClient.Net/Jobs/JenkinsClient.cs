using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using JenkinsClient.Net.Models;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net
{
	public partial class JenkinsClient
	{
		public IFlurlRequest GetJobUrl() => GetBaseUrl().AppendPathSegment("job");

		// ReSharper disable once CoVariantArrayConversion
		public IFlurlRequest GetJobUrl(params string[] paths) => GetJobUrl().AppendPathSegments(paths);

		public IFlurlRequest GetJobsApiUrl(string url) => GetBaseUrl(url, "api/json");

		public IFlurlRequest GetJobsUrl(string url, string path) => GetJobsApiUrl(url).AppendPathSegments(path, "api/json");

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
			return await GetJobsApiUrl(jobUrl)
				.GetJsonAsync<JobInformation>()
				.ConfigureAwait(false);
		}

		public async Task<string> GetJobConfigurationAsync(string jobName)
		{
			return await GetJobUrl(jobName, "config.xml")
				.GetStringAsync()
				.ConfigureAwait(false);
		}

		public async Task<bool> SetJobConfigurationAsync(string jobName, string xml)
		{
			var response = await GetJobUrl(jobName, "config.xml")
				.PostAsync(new StringContent(xml))
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}

		public async Task<bool> DeleteJobAsync(string jobName)
		{
			var response = await GetJobUrl(jobName, "doDelete")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}

		public async Task<string> GetJobDescriptionAsync(string jobName)
		{
			return await GetJobUrl(jobName, "description")
				.GetStringAsync()
				.ConfigureAwait(false);
		}

		public async Task<bool> SetJobDescriptionAsync(string jobName, string description)
		{
			var data = new
			{
				description
			};

			var response = await GetJobUrl(jobName, "description")
				.PostUrlEncodedAsync(data)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}

		public async Task<string> StartBuildAsync(string jobName)
		{
			var response = await GetJobUrl(jobName, "build")
				.SetQueryParam("delay", "0sec")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return response.Headers.Location.ToString();
		}

		public async Task<string> StartBuildWithParametersAsync(string jobName, IDictionary<string, string> parameters)
		{
			var response = await GetJobUrl(jobName, "buildWithParameters")
				.SetQueryParams(parameters)
				.SetQueryParam("delay", "0sec")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return response.Headers.Location.ToString();
		}

		public async Task<bool> PollScmBuildAsync(string jobName)
		{
			var response = await GetJobUrl(jobName, "polling")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}

		public async Task<bool> EnableJobAsync(string jobName)
		{
			var response = await GetJobUrl(jobName, "enable")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}

		public async Task<bool> DisableJobAsync(string jobName)
		{
			var response = await GetJobUrl(jobName, "disable")
				.PostAsync(s_emptyHttpContent)
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}
	}
}
