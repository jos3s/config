﻿using System.ComponentModel;

using config.Settings;

using Spectre.Console.Cli;

namespace config.Features.AppSettings.Settings;
internal class GenereateKeysSettings : BaseSettings
{
    [CommandOption("-s|--select")]
    [DefaultValue(false)]
    [Description("Select which keys will be generated")]
    public bool SelectKeys { get; set; }

    [CommandOption("-j|--json")]
    [DefaultValue(false)]
    [Description("Generate keys for json file")]
    public bool Json { get; set; }

    [CommandOption("--display")]
    [DefaultValue(false)]
    [Description("Display each line in turn")]
    public bool DisplayPerLines { get; set; }
}