using Spectre.Console.Cli;

namespace config.Settings.AppSettings;
internal class CreateKeySettings : CommandSettings
{
    [CommandArgument(0, "[GROUP]")]
    public string GroupName { get; set; }

    [CommandArgument(1, "[KEY]")]
    public string KeyName { get; set; }

    [CommandArgument(2, "[VALUE]")]
    public string KeyValue { get; set; }
}
