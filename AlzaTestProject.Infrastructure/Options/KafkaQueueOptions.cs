using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Services.Kafka.Options
{
	public class KafkaQueueOptions
	{
		public string Topic { get; set; } = string.Empty;
		public string GroupId { get; set; } = string.Empty;
	}
}
