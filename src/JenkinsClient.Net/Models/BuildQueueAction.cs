using System.Collections.Generic;

namespace JenkinsClient.Net.Models
{
	public class BuildQueueAction : HasClass
	{
		public IEnumerable<BuildQueueCause> Causes { get; set; }
	}
}