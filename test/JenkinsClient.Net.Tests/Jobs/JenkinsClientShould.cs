using System.Linq;
using System.Threading.Tasks;
using Xunit;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net.Tests
{
	public partial class JenkinsClientShould
	{
		[Fact]
		public async Task GetJobsAsync()
		{
			var result = await _client.GetJobsAsync().ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Fact]
		public async Task GetJobInformationsAsync()
		{
			var result = await _client.GetJobInformationsAsync().ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Fact]
		public async Task GetJobInformationAsync()
		{
			var results = await _client.GetJobsAsync().ConfigureAwait(false);
			var firstResult = results.FirstOrDefault();
			if (firstResult == null)
			{
				return;
			}

			var result = await _client.GetJobInformationAsync(firstResult.Url).ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Fact]
		public async Task GetJobConfigurationAsync()
		{
			var results = await _client.GetJobsAsync().ConfigureAwait(false);
			var firstResult = results.FirstOrDefault();
			if (firstResult == null)
			{
				return;
			}

			var result = await _client.GetJobConfigurationAsync(firstResult.Name).ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Fact]
		public async Task GetJobDescriptionAsync()
		{
			var results = await _client.GetJobsAsync().ConfigureAwait(false);
			var firstResult = results.FirstOrDefault();
			if (firstResult == null)
			{
				return;
			}

			var result = await _client.GetJobDescriptionAsync(firstResult.Name).ConfigureAwait(false);
			Assert.NotNull(result);
		}
	}
}
