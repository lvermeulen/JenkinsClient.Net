using System.Threading.Tasks;
using Xunit;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net.Tests
{
	public partial class JenkinsClientShould
	{
		[Fact]
		public async Task GetXBuildQueueAsync()
		{
			var result = await _client.GetBuildQueueAsync().ConfigureAwait(false);
			Assert.NotNull(result);
		}
	}
}
