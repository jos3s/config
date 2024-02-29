using config.Features._Shared;

using Spectre.Console.Cli;

using System.ComponentModel;

namespace config.Features.ConnectionStrings;

internal class ConnectionStringsSettings : BaseSettings
{
    [CommandArgument(0, "<user>")]
    public string User { get; set; }

    [CommandArgument(1, "<password>")]
    public string Password { get; set; }

    [CommandArgument(2, "<instance>")]
    public string Instance { get; set; }

    [CommandOption("-s|--select")]
    [DefaultValue(false)]
    [Description("Select which database connection string will be generated for")]
    public bool SelectDatabases { get; set; }

    [CommandOption("-d|--display")]
    [DefaultValue(true)]
    [Description("Display result in the terminal")]
    public bool Display { get; set; }

    [CommandOption("-j | --json")]
    [DefaultValue(false)]
    public bool Json { get; set; }

    [CommandOption("-e| --export <PATH>")]
    public string? ExportPath { get; set; }

    [CommandOption("--search <PATH>")]
    [Description("Directory to search appsettings.json or .config")]
    public string SearchDirectory { get; set; } = string.Empty;

    [CommandOption("-o|--overwrite")]
    [DefaultValue(false)]
    [Description("Overwrite the values of app settings")]
    public bool Overwrite { get; set; }
}
