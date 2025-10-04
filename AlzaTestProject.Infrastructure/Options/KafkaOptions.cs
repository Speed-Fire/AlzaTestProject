using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Services.Kafka.Options
{
	public class KafkaOptions
	{
		public string BootstrapServers { get; set; } = string.Empty;

		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? SaslMechanism { get; set; } = "PLAIN"; // PLAIN, SCRAM-SHA-256, SCRAM-SHA-512
		public string? SecurityProtocol { get; set; } = "SaslPlaintext"; // or "SaslSsl"
	}
}
