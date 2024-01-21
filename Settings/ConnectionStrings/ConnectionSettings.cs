using Spectre.Console.Cli;

using System.ComponentModel;

namespace config.Settings.ConnectionStrings
{
    internal class ConnectionSettings : CommandSettings
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

        [CommandOption("--display")]
        [DefaultValue(false)]
        [Description("Display each line in turn")]
        public bool DisplayStatus { get; set; }

        [CommandOption("-j | --json")]
        [DefaultValue(false)]
        public bool JsonFormat { get; set; }
    }
}
