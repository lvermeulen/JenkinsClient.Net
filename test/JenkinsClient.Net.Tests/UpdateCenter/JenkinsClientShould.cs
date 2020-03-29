using System.Linq;
using System.Threading.Tasks;
using Xunit;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net.Tests
{
	public partial class JenkinsClientShould
	{
		[Fact]
		public async Task GetCoreSourceUpdateCenterAsync()
		{
			var result = await _client.GetCoreSourceUpdateCenterAsync().ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Fact]
		public async Task GetUpdateCenterSitesAsync()
		{
			var result = await _client.GetUpdateCenterSitesAsync().ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Fact]
		public async Task GetUpdateCenterJobAsync()
		{
			var results = _client.GetUpdateCenterJobNames();
			var firstResult = results.FirstOrDefault();
			if (firstResult == null)
			{
				return;
			}

			var result = await _client.GetUpdateCenterJobAsync(firstResult).ConfigureAwait(false);
			Assert.NotNull(result);
		}
	}
}
