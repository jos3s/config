using System.Text.Json.Serialization;

namespace config.Models;
internal class AppSettings
{
    [JsonPropertyName("redis")]
    public List<AppKey> Redis { get; set; }

    [JsonPropertyName("ms")]
    public List<AppKey> Microservices { get; set; }

    [JsonPropertyName("logs")]
    public List<AppKey> Logs { get; set; }
}
