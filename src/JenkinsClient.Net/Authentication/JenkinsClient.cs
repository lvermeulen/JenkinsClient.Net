using JenkinsClient.Net.Common.Authentication;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net
{
	public partial class JenkinsClient
	{
		public JenkinsClient(string url, BasicAuthentication basic)
			: this(url)
		{
			_auth = basic;
		}

		public JenkinsClient(string url, ApiTokenAuthentication apiToken)
			: this(url)
		{
			_auth = apiToken;
		}
	}
}
