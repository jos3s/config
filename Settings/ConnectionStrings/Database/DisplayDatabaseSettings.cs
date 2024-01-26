using Spectre.Console.Cli;

namespace config.Settings.ConnectionStrings.Database;
internal class DisplayDatabaseSettings : CommandSettings
{
    [CommandOption("-j|--json")]
    public bool DisplayInJson { get; set; }
}
