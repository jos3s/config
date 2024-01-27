using System.Text.Json.Serialization;

namespace config.Models;

internal class SettingsGroup
{
    [JsonPropertyName("group")]
    public string GroupName { get; set; }

    [JsonPropertyName("keys")]
    public List<Setting> Keys { get; set; }
}
