using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using JenkinsClient.Net.Common;
using JenkinsClient.Net.Models;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net
{
	public partial class JenkinsClient
	{
		private static readonly Dictionary<string, int> s_updateCenterJobs = new Dictionary<string, int>
		{
			["hudson.model.UpdateCenter$ConnectionCheckJob@38aaa1"] = 0,
			["hudson.model.UpdateCenter$InstallationJob@1d18611[plugin = Trilead API]"] = 1,
			["hudson.model.UpdateCenter$InstallationJob@19c6778[plugin = Folders]"] = 2,
			["hudson.model.UpdateCenter$InstallationJob@1d076c[plugin = Oracle Java S3 Development Kit Installer]"] = 3,
			["hudson.model.UpdateCenter$InstallationJob@769cc3[plugin = Script Security]"] = 4,
			["hudson.model.UpdateCenter$InstallationJob@1a460d2[plugin = Command Agent Launc5er]"] = 5,
			["hudson.model.UpdateCenter$InstallationJob@10d7579[plugin = OWASP Markup Formatter]"] = 6,
			["hudson.model.UpdateCenter$InstallationJob@14920d3[plugin = Structs]"] = 7,
			["hudson.model.UpdateCenter$InstallationJob@1f52c7d[plugin = Pipeline: Step API]"] = 8,
			["hudson.model.UpdateCenter$InstallationJob@1df6321[plugin = Token Macro]"] = 9,
			["hudson.model.UpdateCenter$InstallationJob@1b7749e[plugin = bouncycastle API]"] = 10,
			["hudson.model.UpdateCenter$InstallationJob@1769643[plugin = Build Timeout]"] = 11,
			["hudson.model.UpdateCenter$InstallationJob@1a2c094[plugin = Credentials]"] = 12,
			["hudson.model.UpdateCenter$InstallationJob@1fc61ea[plugin = Plain Credentials]"] = 13,
			["hudson.model.UpdateCenter$InstallationJob@1f9e9cf[plugin = SSH Credentials]"] = 14,
			["hudson.model.UpdateCenter$InstallationJob@aabefd[plugin = Credentials Binding]"] = 15,
			["hudson.model.UpdateCenter$InstallationJob@17b771c[plugin = SCM API]"] = 16,
			["hudson.model.UpdateCenter$InstallationJob@192b428[plugin = Pipeline: API]"] = 17,
			["hudson.model.UpdateCenter$InstallationJob@10590c4[plugin = Timestamper]"] = 18,
			["hudson.model.UpdateCenter$InstallationJob@759dcf[plugin = Pipeline: Supporting APIs]"] = 19,
			["hudson.model.UpdateCenter$InstallationJob@62ca4a[plugin = Durable Task]"] = 20,
			["hudson.model.UpdateCenter$InstallationJob@d1141a[plugin = Pipeline: Nodes and Processes]"] = 21,
			["hudson.model.UpdateCenter$InstallationJob@14b2ef4[plugin = JUnit]"] = 22,
			["hudson.model.UpdateCenter$InstallationJob@1645a77[plugin = Matrix Project]"] = 23,
			["hudson.model.UpdateCenter$InstallationJob@18eec8b[plugin = Resource Disposer]"] = 24,
			["hudson.model.UpdateCenter$InstallationJob@13b5e24[plugin = Workspace Cleanup]"] = 25,
			["hudson.model.UpdateCenter$InstallationJob@10bf3b9[plugin = Ant]"] = 26,
			["hudson.model.UpdateCenter$InstallationJob@7fb84d[plugin = JavaScript GUI Lib: ACE Editor bundle]"] = 27,
			["hudson.model.UpdateCenter$InstallationJob@d9e07d[plugin = JavaScript GUI Lib: jQuery bundles(jQuery and jQuery UI)]"] = 28,
			["hudson.model.UpdateCenter$InstallationJob@1365bbc[plugin = Pipeline: SCM Step]"] = 29,
			["hudson.model.UpdateCenter$InstallationJob@10cfd18[plugin = Pipeline: Groovy]"] = 30,
			["hudson.model.UpdateCenter$InstallationJob@7c8e24[plugin = Pipeline: Job]"] = 31,
			["hudson.model.UpdateCenter$InstallationJob@1155b5e[plugin = Apache HttpComponents Client 4.x API]"] = 32,
			["hudson.model.UpdateCenter$InstallationJob@151a3b[plugin = Display URL API]"] = 33,
			["hudson.model.UpdateCenter$InstallationJob@63cf1c[plugin = Mailer]"] = 34,
			["hudson.model.UpdateCenter$InstallationJob@b272da[plugin = Pipeline: Basic Steps]"] = 35,
			["hudson.model.UpdateCenter$InstallationJob@15c13f9[plugin = Gradle]"] = 36,
			["hudson.model.UpdateCenter$InstallationJob@15014eb[plugin = Pipeline: Milestone Step]"] = 37,
			["hudson.model.UpdateCenter$InstallationJob@1a73832[plugin = Jackson 2 API]"] = 38,
			["hudson.model.UpdateCenter$InstallationJob@3ebb3e[plugin = Pipeline: Input Step]"] = 39,
			["hudson.model.UpdateCenter$InstallationJob@112b1db[plugin = Pipeline: Stage Step]"] = 40,
			["hudson.model.UpdateCenter$InstallationJob@a2055e[plugin = Pipeline Graph Analysis]"] = 41,
			["hudson.model.UpdateCenter$InstallationJob@d6cc10[plugin = Pipeline: REST API]"] = 42,
			["hudson.model.UpdateCenter$InstallationJob@15265ff[plugin = JavaScript GUI Lib: Handlebars bundle]"] = 43,
			["hudson.model.UpdateCenter$InstallationJob@12299a7[plugin = JavaScript GUI Lib: Moment.js bundle]"] = 44,
			["hudson.model.UpdateCenter$InstallationJob@184f068[plugin = Pipeline: Stage View]"] = 45,
			["hudson.model.UpdateCenter$InstallationJob@1a81b84[plugin = Pipeline: Build Step]"] = 46,
			["hudson.model.UpdateCenter$InstallationJob@109f913[plugin = Pipeline: Model API]"] = 47,
			["hudson.model.UpdateCenter$InstallationJob@51ae74[plugin = Pipeline: Declarative Extension Points API]"] = 48,
			["hudson.model.UpdateCenter$InstallationJob@fc1626[plugin = JSch dependency]"] = 49,
			["hudson.model.UpdateCenter$InstallationJob@1df4346[plugin = Git client]"] = 50,
			["hudson.model.UpdateCenter$InstallationJob@19af30f[plugin = GIT server]"] = 51,
			["hudson.model.UpdateCenter$InstallationJob@49dfc6[plugin = Pipeline: Shared Groovy Libraries]"] = 52,
			["hudson.model.UpdateCenter$InstallationJob@32aa4f[plugin = Branch API]"] = 53,
			["hudson.model.UpdateCenter$InstallationJob@18c043c[plugin = Pipeline: Multibranch]"] = 54,
			["hudson.model.UpdateCenter$InstallationJob@15c9a89[plugin = Authentication Tokens API]"] = 55,
			["hudson.model.UpdateCenter$InstallationJob@151cfcb[plugin = Docker Commons]"] = 56,
			["hudson.model.UpdateCenter$InstallationJob@66677c[plugin = Docker Pipeline]"] = 57,
			["hudson.model.UpdateCenter$InstallationJob@1123e68[plugin = Pipeline: Stage Tags Metadata]"] = 58,
			["hudson.model.UpdateCenter$InstallationJob@156fd1[plugin = Pipeline: Declarative Agent API]"] = 59,
			["hudson.model.UpdateCenter$InstallationJob@165cd8f[plugin = Pipeline: Declarative]"] = 60,
			["hudson.model.UpdateCenter$InstallationJob@8d0698[plugin = Lockable Resources]"] = 61,
			["hudson.model.UpdateCenter$InstallationJob@a29edf[plugin = Pipeline]"] = 62,
			["hudson.model.UpdateCenter$InstallationJob@1b20c7d[plugin = GitHub API]"] = 63,
			["hudson.model.UpdateCenter$InstallationJob@1aa1b1[plugin = Git]"] = 64,
			["hudson.model.UpdateCenter$InstallationJob@f1db2c[plugin = GitHub]"] = 65,
			["hudson.model.UpdateCenter$InstallationJob@45e60b[plugin = GitHub Branch Source]"] = 66,
			["hudson.model.UpdateCenter$InstallationJob@18bc01a[plugin = Pipeline: GitHub Groovy Libraries]"] = 67,
			["hudson.model.UpdateCenter$InstallationJob@67eef3[plugin = Pipeline: Stage View]"] = 68,
			["hudson.model.UpdateCenter$InstallationJob@bd7268[plugin = Git]"] = 69,
			["hudson.model.UpdateCenter$InstallationJob@17ba88a[plugin = MapDB API]"] = 70,
			["hudson.model.UpdateCenter$InstallationJob@2d34af[plugin = Subversion]"] = 71,
			["hudson.model.UpdateCenter$InstallationJob@17a4eaf[plugin = SSH Build Agents]"] = 72,
			["hudson.model.UpdateCenter$InstallationJob@35469a[plugin = Matrix Authorization Strategy]"] = 73,
			["hudson.model.UpdateCenter$InstallationJob@ee99ae[plugin = PAM Authentication]"] = 74,
			["hudson.model.UpdateCenter$InstallationJob@1eb5990[plugin = LDAP]"] = 75,
			["hudson.model.UpdateCenter$InstallationJob@957574[plugin = Email Extension]"] = 76,
			["hudson.model.UpdateCenter$InstallationJob@fcf92a[plugin = Mailer]"] = 77,
			["hudson.model.UpdateCenter$CompleteBatchJob@4db7a6"] = 78,
			["hudson.model.UpdateCenter$InstallationJob@18e4680[plugin = MSBuild]"] = 79,
			["hudson.model.UpdateCenter$CompleteBatchJob@637173"] = 80,
			["hudson.model.UpdateCenter$InstallationJob@11afc6d[plugin = Next Build Number]"] = 81,
			["hudson.model.UpdateCenter$CompleteBatchJob@c66e91"] = 82
		};

		private IFlurlRequest GetUpdateCenterUrl() => GetBaseUrl().AppendPathSegment("updateCenter");

		private IFlurlRequest GetUpdateCenterApiUrl() => GetUpdateCenterUrl().AppendPathSegment("api/json");

		private IFlurlRequest GetUpdateCenterApiUrl(string path) => GetUpdateCenterUrl()
			.AppendPathSegment(path)
			.AppendPathSegment("api/json");

		private IFlurlRequest GetUpdateCenterJobApiUrl(string updateCenterJobName) => GetUpdateCenterUrl()
			.AppendPathSegment("jobs")
			.AppendPathSegment(GetUpdateCenterJobNameIndex(updateCenterJobName))
			.AppendPathSegment("api/json");

		public async Task<UpdateCenterCoreSource> GetCoreSourceUpdateCenterAsync()
		{
			return await GetUpdateCenterApiUrl("coreSource")
				.GetJsonAsync<UpdateCenterCoreSource>()
				.ConfigureAwait(false);
		}

		public async Task<IEnumerable<UpdateCenterSite>> GetUpdateCenterSitesAsync()
		{
			return await GetUpdateCenterApiUrl()
				.SetQueryParam("tree", "sites[id,url]")
				.GetJsonNamedNodeAsync<IEnumerable<UpdateCenterSite>>("sites")
				.ConfigureAwait(false);
		}

		public IEnumerable<string> GetUpdateCenterJobNames() => s_updateCenterJobs.Keys;

		private int GetUpdateCenterJobNameIndex(string updateCenterJobName) => s_updateCenterJobs[updateCenterJobName];

		public async Task<UpdateCenterJob> GetUpdateCenterJobAsync(string updateCenterJobName)
		{
			return await GetUpdateCenterJobApiUrl(updateCenterJobName)
				.GetJsonAsync<UpdateCenterJob>()
				.ConfigureAwait(false);
		}
	}
}
