using System.IO;
using System.Threading.Tasks;
using JenkinsClient.Net.Common.Authentication;
using Microsoft.Extensions.Configuration;
using Xunit;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net.Tests
{
	public partial class JenkinsClientShould
	{
		public IConfiguration GetConfiguration()
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.Build();
		}

		[Fact]
		public async Task ConnectWithBasicAuthentication()
		{
			var configuration = GetConfiguration();

			var client = new JenkinsClient(configuration["url"], new BasicAuthentication(configuration["userName"], configuration["password"]));
			string result = await client.GetVersionAsync().ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Fact]
		public async Task ConnectWithApiTokenAuthentication()
		{
			var configuration = GetConfiguration();

			var client = new JenkinsClient(configuration["url"], new ApiTokenAuthentication(configuration["userName"], configuration["apiToken"]));
			string result = await client.GetVersionAsync().ConfigureAwait(false);
			Assert.NotNull(result);
		}
	}
}
