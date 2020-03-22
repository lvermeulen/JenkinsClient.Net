using System.Collections.Generic;

namespace JenkinsClient.Net.Models
{
	public class SystemInformation : HasClass
	{
		public IEnumerable<AssignedLabel> AssignedLabels { get; set; }
		public string Mode { get; set; }
		public string NodeDescription { get; set; }
		public string NodeName { get; set; }
		public int? NumExecutors { get; set; }
		public string Description { get; set; }
		public IEnumerable<Job> Jobs { get; set; }
		public OverallLoadStatistics OverallLoad { get; set; }
		public PrimaryView PrimaryView { get; set; }
		public bool? QuietingDown { get; set; }
		public int? SlaveAgentPort { get; set; }
		public UnlabeledLoadStatistics UnlabeledLoad { get; set; }
		public string Url { get; set; }
		public bool? UseCrumbs { get; set; }
		public bool? UseSecurity { get; set; }
		public IEnumerable<View> Views { get; set; }
	}
}
