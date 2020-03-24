using System;
using Flurl.Http;

namespace JenkinsClient.Net.Common.Authentication
{
	public static class FlurlRequestExtensions
	{
		public static IFlurlRequest WithAuthentication(this IFlurlRequest request, AuthenticationMethod auth)
		{
			if (auth.GetType() == typeof(BasicAuthentication))
			{
				var basic = (BasicAuthentication)auth;
				return request.WithBasicAuth(basic.UserName, basic.Password);
			}

			if (auth.GetType() == typeof(ApiTokenAuthentication))
			{
				var apiToken = (ApiTokenAuthentication)auth;
				return request.WithApiTokenAuth(apiToken.UserName, apiToken.ApiToken);
			}

			throw new InvalidOperationException("Unknown authentication method");
		}

		public static T WithApiTokenAuth<T>(this T clientOrRequest, string username, string apiToken)
			where T : IHttpSettingsContainer
		{
			return clientOrRequest.WithBasicAuth(username, apiToken);
		}
	}
}
