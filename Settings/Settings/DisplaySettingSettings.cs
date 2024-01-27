using System.ComponentModel;
using Spectre.Console.Cli;

namespace config.Settings.Settings;
internal class DisplaySettingSettings : CommandSettings
{
    [CommandOption("-j|--json")]
    [DefaultValue(false)]
    public bool DisplayInJson { get; set; }
}
