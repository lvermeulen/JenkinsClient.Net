namespace JenkinsClient.Net.Models
{
	public class Build : HasClass
	{
		public int? Number { get; set; }
		public string Url { get; set; }
	}
}