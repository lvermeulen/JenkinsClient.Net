using System;
using System.Collections.Generic;

namespace JenkinsClient.Net.Models
{
	public class BuildQueueItem : HasClass
	{
		public IEnumerable<BuildQueueAction> Actions { get; set; }
		public bool Blocked { get; set; }
		public bool Buildable { get; set; }
		public int Id { get; set; }
		public long InQueueSince { get; set; }
		public string Params { get; set; }
		public bool Stuck { get; set; }
		public BuildQueueTask Task { get; set; }
		public string Url { get; set; }
		public string Why { get; set; }
		public long BuildableStartMilliseconds { get; set; }
		public TimeSpan BuildableStart => TimeSpan.FromMilliseconds(BuildableStartMilliseconds);
	}
}