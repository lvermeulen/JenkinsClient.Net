using System.Collections.Generic;

namespace JenkinsClient.Net.Models
{
	public class UpdateCenterCoreSource : HasClass
	{
		public IEnumerable<UpdateCenterAvailable> Availables { get; set; }
		public string ConnectionCheckUrl { get; set; }
		public long DataTimestamp { get; set; }
		public bool HasUpdates { get; set; }
		public string Id { get; set; }
		public IEnumerable<Update> Updates { get; set; }
		public string Url { get; set; }
	}
}
