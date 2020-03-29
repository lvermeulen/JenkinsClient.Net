using System.Collections.Generic;

namespace JenkinsClient.Net.Models
{
	public class UpdateCenter : HasClass
	{
		public IEnumerable<UpdateCenterAvailable> Availables { get; set; }
		public IEnumerable<UpdateCenterJob> Jobs { get; set; }
		public bool RestartRequiredForCompletion { get; set; }
		public IEnumerable<UpdateCenterSite> Sites { get; set; }
	}
}
