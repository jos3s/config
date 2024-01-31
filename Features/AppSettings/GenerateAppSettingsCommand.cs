using config.Features.Settings.Shared;
using config.Singleton;
using config.Transaction;
using config.Utils.Display;
using config.Utils.Extensions;
using config.Utils.Messages;
using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Features.AppSettings;
internal class GenerateAppSettingsCommand : Command<GenerateKeysSettings>
{
    public override int Execute(CommandContext context, GenerateKeysSettings settings)
    {
        var appSettings = SettingsSingleton.Instance.Lines();

        var keys = appSettings.SelectMany(x => x.Keys.Select(x => x.Key));

        if (settings.SelectKeys)
        {
            keys = MultiSelectDisplay.ExecuteForSettingsGroup(appSettings, "keys");
        }

        var strings = CreateListResult(appSettings, keys, settings.Json);

        if (!string.IsNullOrEmpty(settings.ExportPath) || !string.IsNullOrWhiteSpace(settings.ExportPath))
            CreateExportFile(settings, strings);

        if (settings.DisplayPerLines)
        {
            RepeatableStatusDisplay.Run(strings,
                SettingsMsg.INF001,
                SettingsMsg.INF007,
                SettingsMsg.INF006);
        }
        else
        {
            var rows = strings.Select(x => new Text(x));

            AnsiConsole.Write(new Rows(rows));
        }

        return 0;
    }

    private static void CreateExportFile(GenerateKeysSettings settings, IEnumerable<string> strings)
    {
        CreateFileTRA.ValidatePath(settings.ExportPath);

        settings.ExportPath += @"\appsettings.txt";

        AnsiConsole.Status()
            .Start(FileMsg.INF005, ctx =>
            {
                // Simulate some work
                ctx.Status(FileMsg.INF001);
                ctx.Spinner(Spinner.Known.Balloon);
                ctx.SpinnerStyle(Style.Parse("green"));
                Thread.Sleep(1000);

                // Update the status and spinner
                ctx.Status(FileMsg.INF002);
                ctx.Spinner(Spinner.Known.Balloon);
                ctx.SpinnerStyle(Style.Parse("green"));
                Thread.Sleep(1000);


                ctx.Status(FileMsg.INF003);
                ctx.Spinner(Spinner.Known.Balloon);
                ctx.SpinnerStyle(Style.Parse("green"));
                ctx.Refresh();
                Thread.Sleep(1000);

                CreateFileTRA.WriteLinesInFile(settings.ExportPath , strings);

                var panel = new Panel(
                string.Format(FileMsg.INF004, $"[blue]{settings.ExportPath}[/]"))
                    .Formatted();

                Thread.Sleep(1000);
                AnsiConsole.Write(panel);
            });
    }

    private IEnumerable<string> CreateListResult(List<SettingsGroupModel> appSettings, IEnumerable<string> keys, bool json)
    {
        var output = new List<string>();

        foreach (var appSettingsGroup in appSettings)
        {
            foreach (var key in appSettingsGroup.Keys)
            {
                if (keys.Contains(key.Key)) output.Add(!json ? key.ToConfig() : key.ToJson());
            }
        }

        return output;
    }
}
