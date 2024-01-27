using config.Models;
using config.Settings.AppSettings;
using config.Singleton;
using config.Utils.Display;
using config.Utils.Messages;
using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Commands.AppSettings;
internal class GenerateAppSettingsCommand : Command<GenereateKeysSettings>
{
    public override int Execute(CommandContext context, GenereateKeysSettings settings)
    {
        var appSettings = SettingsSingleton.Instance.Lines();

        var keys = appSettings.SelectMany(x => x.Keys.Select(x => x.Key));

        if (settings.SelectKeys)
        {
            keys = DisplayMultiSelection(appSettings);
        }

        var strings = CreateListResult(appSettings, keys, settings.Json);

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

    private static IEnumerable<string> DisplayMultiSelection(IEnumerable<SettingsGroup> appSettings)
    {
        var multiSelection = new MultiSelectionPrompt<string>()
            .Title("Select [green]keys[/]:")
            .Required()
            .PageSize(10)
            .InstructionsText("[grey](Press [blue]<space>[/] to toggle a database, " +
                              "[green]<enter>[/] to accept)[/]");

        foreach (var group in appSettings)
        {
            multiSelection.AddChoiceGroup(group.GroupName, group.Keys.Select(x=> x.Key));
        }

        return AnsiConsole.Prompt(multiSelection);
    }

    private IEnumerable<string> CreateListResult(List<SettingsGroup> appSettings, IEnumerable<string> keys, bool json)
    {
        var output = new List<string>();

        foreach (var appSettingsGroup in appSettings)
        {
            foreach (var key in appSettingsGroup.Keys)
            {
                if(keys.Contains(key.Key)) output.Add(!json ? key.ToConfig() : key.ToJson());
            }
        }

        return output;
    }
}
