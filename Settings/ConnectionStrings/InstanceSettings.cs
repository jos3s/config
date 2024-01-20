using Spectre.Console.Cli;

namespace config.Settings.ConnectionStrings
{
    internal class InstanceSettings : PasswordSettings
    {
        [CommandArgument(2, "<instance>")]
        public string Instance { get; set; }
    }
}
