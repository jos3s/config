using System.Text.Json.Serialization;

namespace config.Features.Settings.Shared;

internal class SettingsGroupModel
{
    [JsonPropertyName("group")]
    public string GroupName { get; set; }

    [JsonPropertyName("keys")]
    public List<SettingModel> Keys { get; set; }
}
