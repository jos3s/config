using Spectre.Console.Cli;
using System.ComponentModel;

namespace config.Settings.ConnectionStrings
{
    internal class ConnectionSettings : InstanceSettings
    {
        [CommandOption("-d|--databases")]
        [DefaultValue(false)]
        [Description("Select which database connection string will be generated for")]
        public bool SelectDatabases { get; set; }

        [CommandOption("--display")]
        [DefaultValue(false)]
        [Description("Display each line in turn")]
        public bool DisplayStatus { get; set; }
    }
}
