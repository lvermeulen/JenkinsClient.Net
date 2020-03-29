namespace JenkinsClient.Net.Models
{
	public class BuildQueueCause : HasClass
	{
		public string ShortDescription { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
	}
}