using System.Threading.Tasks;
using Xunit;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net.Tests
{
	public partial class JenkinsClientShould
	{
		[Fact]
		public async Task GetSystemInformationAsync()
		{
			var result = await _client.GetSystemInformationAsync().ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Fact]
		public async Task GetVersionAsync()
		{
			string result = await _client.GetVersionAsync().ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Fact]
		public async Task GetSecurityCrumbAsync()
		{
			var result = await _client.GetSecurityCrumbAsync().ConfigureAwait(false);
			Assert.NotNull(result);
		}
	}
}
