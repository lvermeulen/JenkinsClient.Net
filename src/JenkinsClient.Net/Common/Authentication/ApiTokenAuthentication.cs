namespace JenkinsClient.Net.Common.Authentication
{
	public class ApiTokenAuthentication : AuthenticationMethod
	{
		public string UserName { get; set; }
		public string ApiToken { get; set; }

		public ApiTokenAuthentication(string userName, string apiToken)
		{
			UserName = userName;
			ApiToken = apiToken;
		}
	}
}
