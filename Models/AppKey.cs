using System.Text.Json.Serialization;

namespace config.Models;
internal class AppKey
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    public override string ToString()
    {
        string output = $"<add key=\"{Key}\" value=\"{Value}\"/>";

        return output;
    }
}
