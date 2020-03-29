using System.Collections.Generic;

namespace JenkinsClient.Net.Models
{
	public class BuildQueue : HasClass
	{
		public IEnumerable<BuildQueueItem> DiscoverableItems { get; set; }
		public IEnumerable<BuildQueueItem> Items { get; set; }
	}
}
