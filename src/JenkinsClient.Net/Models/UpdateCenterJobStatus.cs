namespace JenkinsClient.Net.Models
{
	public class UpdateCenterJobStatus : HasClass
	{
		public bool Success { get; set; }
		public string Type { get; set; }
	}
}