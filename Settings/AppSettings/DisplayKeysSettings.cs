using System.ComponentModel;
using Spectre.Console.Cli;

namespace config.Settings.AppSettings;
internal class DisplayKeysSettings : CommandSettings
{
    [CommandOption("-j|--json")]
    [DefaultValue(false)]
    public bool DisplayInJson { get; set; }
}
