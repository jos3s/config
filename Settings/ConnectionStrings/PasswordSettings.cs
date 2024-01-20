using Spectre.Console.Cli;

namespace config.Settings.ConnectionStrings
{
    internal class PasswordSettings : UserSettings
    {

        [CommandArgument(1, "<password>")]
        public string Password { get; set; }
    }
}
