using System.ComponentModel;
using Spectre.Console.Cli;

namespace config.Settings;
internal class AppSettingsSettings : CommandSettings
{
    [CommandOption("-s|--select")]
    [DefaultValue(false)]
    public bool SelectKeys { get; set; }

    [CommandOption("-j|--json")]
    [DefaultValue(false)]
    public bool Json { get; set; }
}
