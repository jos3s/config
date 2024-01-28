using System.Text.Json.Serialization;

namespace config.Features.Database.Models
{
    internal class Database
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("providerName")]
        public string ProviderName { get; set; }

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
