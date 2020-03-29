using System.Collections.Generic;
using JenkinsClient.Net.Models;

namespace JenkinsClient.Net.Common.Converters
{
	public class JenkinsModesConverter : JsonEnumConverter<JenkinsModes>
	{
		public override Dictionary<JenkinsModes, string> Map { get; } = new Dictionary<JenkinsModes, string>
		{
			[JenkinsModes.Normal] = "NORMAL",
			[JenkinsModes.Exclusive] = "Exclusive"
		};

		public override string Description { get; } = "Jenkins mode";
	}
}
