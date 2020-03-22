namespace JenkinsClient.Net.Models
{
	public class Job : HasClass
	{
		public string Name { get; set; }
		public string Url { get; set; }
		public string Color { get; set; }
	}
}