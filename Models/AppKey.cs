using System.Text.Json.Serialization;

namespace config.Models;
internal class AppKey
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    public string ToConfig()
    {
        string output = $"<add key=\"{Key}\" value=\"{Value}\"/>";

        return output;
    }

    public string ToJson()
    {
        int.TryParse(Value, out int intValue);
        bool.TryParse(Value, out bool boolValue);
        float.TryParse(Value, out float floatValue);

        string output = $"\"{Key}\":";

        if (Value.Equals(intValue.ToString()))
            output += $"{intValue}";
        else if (Value.Equals(boolValue.ToString(), StringComparison.InvariantCultureIgnoreCase))
            output += $"{boolValue}";
        else if (Value.Equals(floatValue.ToString()))
            output += $"{floatValue}";

        return output;
    }
}
