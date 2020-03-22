using Newtonsoft.Json;

namespace JenkinsClient.Net.Models
{
	public abstract class HasClass
	{
		[JsonProperty("_class")]
		public string Class { get; set; }
	}
}
