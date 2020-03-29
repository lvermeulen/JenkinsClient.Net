namespace JenkinsClient.Net.Models
{
	public class UpdateCenterJob : HasClass
	{
		public string ErrorMessage { get; set; }
		public int Id { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
		public UpdateCenterJobStatus Status { get; set; }
		public UpdateCenterJobPlugin Plugin { get; set; }
	}
}