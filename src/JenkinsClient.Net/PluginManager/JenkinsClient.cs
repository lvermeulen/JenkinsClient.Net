using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using JenkinsClient.Net.Models;

// ReSharper disable once CheckNamespace
namespace JenkinsClient.Net
{
	public partial class JenkinsClient
	{
		public IFlurlRequest GetPluginManagerUrl() => GetBaseUrl().AppendPathSegment("pluginManager");

		public IFlurlRequest GetPluginManagerUrl(string path) => GetPluginManagerUrl().AppendPathSegment(path);

		public async Task<bool> UploadPluginAsync(string pluginFileName)
		{
			var response = await GetPluginManagerUrl("uploadPlugin")
				.PostMultipartAsync(content => content.AddFile(Path.GetFileName(pluginFileName), pluginFileName))
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}

		public async Task<IEnumerable<PluginState>> PrevalidateConfigAsync(string xml)
		{
			var response = await GetPluginManagerUrl("prevalidateConfig")
				.WithHeader("Content-Type", "application/xml")
				.PostAsync(new StringContent(xml))
				.ConfigureAwait(false);

			return await HandleResponseAsync<IEnumerable<PluginState>>(response).ConfigureAwait(false);
		}

		public async Task<bool> InstallNecessaryPluginsAsync(string xml)
		{
			var response = await GetPluginManagerUrl("installNecessaryPlugins")
				.WithHeader("Content-Type", "application/xml")
				.PostAsync(new StringContent(xml))
				.ConfigureAwait(false);

			return await HandleResponseAsync(response).ConfigureAwait(false);
		}
	}
}
