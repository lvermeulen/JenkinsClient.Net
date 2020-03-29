using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using JenkinsClient.Net.Common;
using JenkinsClient.Net.Models;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net
{
	public partial class JenkinsClient
	{
		public IFlurlRequest GetBuildQueueUrl() => GetBaseUrl().AppendPathSegment("queue").AppendPathSegment("api/json");

		public async Task<BuildQueue> GetBuildQueueAsync()
		{
			return await GetBuildQueueUrl()
				.GetJsonAsync<BuildQueue>()
				.ConfigureAwait(false);
		}
	}
}
