using System.Text.Json.Serialization;
using config.Utils.Messages;

namespace config.Features.Settings.Shared;
internal class SettingModel
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    public string ToConfig()
    {
        return string.Format(StringsFormatedMsg.APPCONFIG, Key, Value);
    }

    public string ToJson()
    {
        int.TryParse(Value, out int intValue);
        bool.TryParse(Value, out bool boolValue);
        float.TryParse(Value, out float floatValue);

        string output = $"\"{Key}\":";

        if (Value.Equals(intValue.ToString()))
            output = string.Format(StringsFormatedMsg.APPJSON, Key, intValue);
        else if (Value.Equals(boolValue.ToString(), StringComparison.InvariantCultureIgnoreCase))
            output = string.Format(StringsFormatedMsg.APPJSON, Key, boolValue);
        else if (Value.Equals(floatValue.ToString()))
            output = string.Format(StringsFormatedMsg.APPJSON, Key, floatValue);
        else
            output = string.Format(StringsFormatedMsg.APPJSON, Key, $"\"{Value}\"");

        return output;
    }
}
