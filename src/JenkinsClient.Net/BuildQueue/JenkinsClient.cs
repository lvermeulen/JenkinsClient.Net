﻿using System.Threading.Tasks;
using Flurl.Http;
using JenkinsClient.Net.Models;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net
{
	public partial class JenkinsClient
	{
		private IFlurlRequest GetBuildQueueUrl() => GetBaseUrl().AppendPathSegment("queue").AppendPathSegment("api/json");

		public async Task<BuildQueue> GetBuildQueueAsync()
		{
			return await GetBuildQueueUrl()
				.GetJsonAsync<BuildQueue>()
				.ConfigureAwait(false);
		}
	}
}
