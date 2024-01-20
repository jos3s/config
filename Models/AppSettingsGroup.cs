using System.Text.Json.Serialization;

namespace config.Models;
internal class AppSettingsGroup
{
    [JsonPropertyName("group")]
    public string GroupName { get; set; }

    [JsonPropertyName("keys")]
    public List<AppKey> Keys { get; set; }
}
