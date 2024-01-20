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
    }
}
