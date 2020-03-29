using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using JenkinsClient.Net.Common;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net
{
	public partial class JenkinsClient
	{
		public IFlurlRequest GetBuildUrl(string jobName, int buildNumber) => GetJobUrl().AppendPathSegment(jobName).AppendPathSegment(buildNumber);

		public IFlurlRequest GetBuildUrl(string jobName, int buildNumber, string path) => GetBuildUrl(jobName, buildNumber).AppendPathSegment(path);

		public async Task<string> GetBuildNumberAsync(string jobName, int buildNumber)
		{
			return await GetBuildUrl(jobName, buildNumber, "buildNumber")
				.GetStringAsync()
				.ConfigureAwait(false);
		}

		public async Task<string> GetBuildTimestampAsync(string jobName, int buildNumber, string format = null, string locale = null)
		{
			return await GetBuildUrl(jobName, buildNumber, "buildTimestamp")
				.SetQueryParam(nameof(format), format)
				.If(locale != null, request => request.WithHeader("Accept-Language", locale))
				.GetStringAsync()
				.ConfigureAwait(false);
		}

		private bool HasMoreData(HttpResponseMessage response)
		{
			if (response.Headers.TryGetValues("X-More-Data", out var hasMoreDataValues))
			{
				return hasMoreDataValues.Any(x => x.Equals("true", StringComparison.OrdinalIgnoreCase));
			}

			return false;
		}

		private int GetStart(HttpResponseMessage httpResponseMessage)
		{
			int start = 0;
			if (httpResponseMessage.Headers.TryGetValues("X-Text-Size", out var textSizeValues))
			{
				string textSize = textSizeValues.FirstOrDefault();
				if (textSize != null)
				{
					start = int.Parse(textSize);
				}
			}

			return start;
		}

		private async Task<string> GetBuildOutputAsync(string jobName, int buildNumber, string path, int start = 0)
		{
			return await GetBuildUrl(jobName, buildNumber, path)
				.SetQueryParam(nameof(start), start)
				.GetStringAsync()
				.ConfigureAwait(false);
		}

		public async Task<string> GetBuildConsoleOutputAsync(string jobName, int buildNumber, int start = 0)
		{
			return await GetBuildOutputAsync(jobName, buildNumber, "logText/progressiveText", start).ConfigureAwait(false);
		}

		public async Task<string> GetBuildHtmlOutputAsync(string jobName, int buildNumber, int start = 0)
		{
			return await GetBuildOutputAsync(jobName, buildNumber, "logText/progressiveHtml", start).ConfigureAwait(false);
		}

		private async Task<IEnumerable<string>> GetCompleteBuildOutputAsync(string jobName, int buildNumber, string path, int start = 0)
		{
			var results = new List<string>();
			bool hasMoreData;
			do
			{
				var response = await GetBuildUrl(jobName, buildNumber, path)
					.SetQueryParam(nameof(start), start)
					.GetAsync()
					.ConfigureAwait(false);

				results.Add(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

				hasMoreData = HasMoreData(response);
				if (hasMoreData)
				{
					start = GetStart(response);
				}

			} while (hasMoreData);

			return results;
		}

		public async Task<IEnumerable<string>> GetCompleteBuildConsoleOutputAsync(string jobName, int buildNumber, int start = 0)
		{
			return await GetCompleteBuildOutputAsync(jobName, buildNumber, "logText/progressiveText", start).ConfigureAwait(false);
		}

		public async Task<IEnumerable<string>> GetCompleteBuildHtmlOutputAsync(string jobName, int buildNumber, int start = 0)
		{
			return await GetCompleteBuildOutputAsync(jobName, buildNumber, "logText/progressiveHtml", start).ConfigureAwait(false);
		}
	}
}
