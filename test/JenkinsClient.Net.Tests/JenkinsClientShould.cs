using System.IO;
using JenkinsClient.Net.Common.Authentication;
using Microsoft.Extensions.Configuration;

namespace JenkinsClient.Net.Tests
{
	public partial class JenkinsClientShould
	{
		private readonly JenkinsClient _client;

		public JenkinsClientShould()
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.Build();

			_client = new JenkinsClient(configuration["url"], new BasicAuthentication(configuration["userName"], configuration["password"]));
		}
	}
}
