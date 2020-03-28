using System.Linq;
using System.Threading.Tasks;
using Xunit;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net.Tests
{
	public partial class JenkinsClientShould
	{
		[Fact]
		public async Task GetBuildNumberAsync()
		{
			var jobs = await _client.GetJobsAsync().ConfigureAwait(false);
			var firstJob = jobs.FirstOrDefault();
			if (firstJob == null)
			{
				return;
			}

			var builds = await _client.GetJobInformationAsync(firstJob.Url).ConfigureAwait(false);
			var firstBuild = builds.Builds.FirstOrDefault();
			if (firstBuild == null)
			{
				return;
			}

			var result = await _client.GetBuildNumberAsync(firstJob.Name, firstBuild.Number).ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Theory]
		[InlineData(null, null)]
		[InlineData("yyyy-dd-MM", null)]
		[InlineData(null, "en-US")]
		public async Task GetBuildTimestampAsync(string format, string locale)
		{
			var jobs = await _client.GetJobsAsync().ConfigureAwait(false);
			var firstJob = jobs.FirstOrDefault();
			if (firstJob == null)
			{
				return;
			}

			var builds = await _client.GetJobInformationAsync(firstJob.Url).ConfigureAwait(false);
			var firstBuild = builds.Builds.FirstOrDefault();
			if (firstBuild == null)
			{
				return;
			}

			var result = await _client.GetBuildTimestampAsync(firstJob.Name, firstBuild.Number, format, locale).ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Fact]
		public async Task GetCompleteBuildConsoleOutputAsync()
		{
			var jobs = await _client.GetJobsAsync().ConfigureAwait(false);
			var firstJob = jobs.FirstOrDefault();
			if (firstJob == null)
			{
				return;
			}

			var builds = await _client.GetJobInformationAsync(firstJob.Url).ConfigureAwait(false);
			var firstBuild = builds.Builds.FirstOrDefault();
			if (firstBuild == null)
			{
				return;
			}

			var result = await _client.GetCompleteBuildConsoleOutputAsync(firstJob.Name, firstBuild.Number).ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Fact]
		public async Task GetCompleteBuildHtmlOutputAsync()
		{
			var jobs = await _client.GetJobsAsync().ConfigureAwait(false);
			var firstJob = jobs.FirstOrDefault();
			if (firstJob == null)
			{
				return;
			}

			var builds = await _client.GetJobInformationAsync(firstJob.Url).ConfigureAwait(false);
			var firstBuild = builds.Builds.FirstOrDefault();
			if (firstBuild == null)
			{
				return;
			}

			var result = await _client.GetCompleteBuildHtmlOutputAsync(firstJob.Name, firstBuild.Number).ConfigureAwait(false);
			Assert.NotNull(result);
		}
	}
}
