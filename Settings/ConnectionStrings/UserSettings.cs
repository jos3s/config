using Spectre.Console.Cli;

namespace config.Settings.ConnectionStrings
{
    internal class UserSettings : CommandSettings
    {
        [CommandArgument(0, "<user>")]
        public string User { get; set; }
    }
}
