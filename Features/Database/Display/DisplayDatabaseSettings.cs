using config.Features._Shared;
using Spectre.Console.Cli;

namespace config.Features.Database.Display;
internal class DisplayDatabaseSettings : BaseSettings
{
    [CommandOption("-j|--json")]
    public bool DisplayInJson { get; set; }
}
