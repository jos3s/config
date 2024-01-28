using config.Features._Shared;
using Spectre.Console.Cli;

namespace config.Features.Settings.Create;
internal class CreateSettingSettings : BaseSettings
{
    [CommandArgument(0, "<group>")]
    public string GroupName { get; set; }

    [CommandArgument(1, "<key>")]
    public string KeyName { get; set; }

    [CommandArgument(2, "<value>")]
    public string KeyValue { get; set; }
}
