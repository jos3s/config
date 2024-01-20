using System.Text.Json.Serialization;

namespace config.Models
{
    internal class ConnectionLine
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("providerName")]
		public string ProviderName { get; set; }

		[JsonPropertyName("connectionString")]
		public ConnectionString ConnectionString { get; set; }
	}
}
