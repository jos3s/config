using System.ComponentModel;
using config.Features._Shared;
using Spectre.Console.Cli;

namespace config.Features.Settings.Display;
internal class DisplaySettingSettings : BaseSettings
{
    [CommandOption("-j|--json")]
    [DefaultValue(false)]
    public bool DisplayInJson { get; set; }
}
