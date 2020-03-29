namespace JenkinsClient.Net.Models
{
	public class BuildQueueTask : HasClass
	{
		public string Name { get; set; }
		public string Url { get; set; }
		public string Color { get; set; }
	}
}