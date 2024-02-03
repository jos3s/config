﻿using config.Features._Shared;

using Spectre.Console.Cli;

using System.ComponentModel;

namespace config.Features.AppSettings;
internal class GenerateKeysSettings : BaseSettings
{
    [CommandOption("-s|--select")]
    [DefaultValue(false)]
    [Description("Select which keys will be generated")]
    public bool SelectKeys { get; set; }

    [CommandOption("-j|--json")]
    [DefaultValue(false)]
    [Description("Generate keys for json file")]
    public bool Json { get; set; }

    [CommandOption("-d|--display")]
    [DefaultValue(true)]
    [Description("Display result in terminal")]
    public bool Display { get; set; }

    [CommandOption("-e| --export <PATH>")]
    public string? ExportPath { get; set; }
}
