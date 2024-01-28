using System.ComponentModel;
using config.Settings;

using Spectre.Console.Cli;

namespace config.Features.Settings.Settings;
internal class DisplaySettingSettings : BaseSettings
{
    [CommandOption("-j|--json")]
    [DefaultValue(false)]
    public bool DisplayInJson { get; set; }
}
