using config.Settings;

using Spectre.Console.Cli;

namespace config.Features.Database.Settings;
internal class DisplayDatabaseSettings : BaseSettings
{
    [CommandOption("-j|--json")]
    public bool DisplayInJson { get; set; }
}
