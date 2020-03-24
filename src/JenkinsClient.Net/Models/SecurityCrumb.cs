namespace JenkinsClient.Net.Models
{
	public class SecurityCrumb : HasClass
	{
		public string Crumb { get; set; }
		public string CrumbRequestField { get; set; }
	}
}
