using System.Text.Json.Serialization;

namespace config.Models
{
	internal class ConnectionString
	{
		[JsonPropertyName("initalCatalog")]
		public string InitalCatalog { get; set; }
		
		[JsonPropertyName("pooling")]
		public string Pooling { get; set; }
		
		[JsonPropertyName("connectionTimeout")]
		public int ConnectTimeout { get; set; }
		
		[JsonPropertyName("aplicationName")]
		public string AplicationName { get; set; }
		
		[JsonPropertyName("encrypt")]
		public string Encrypt { get; set; }

	}
}
