using config.Models;
using config.Models.DTOs;
using config.Settings.AppSettings;
using config.Singleton;
using config.Utils;
using config.Utils.Messages;

using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Commands.AppSettings;
internal class GenerateKeysCommand : Command<GenereateKeysSettings>
{
    public override int Execute(CommandContext context, GenereateKeysSettings settings)
    {
        var appSettingsFile = AppSettingsSingleton.Instance.Lines();

        var groupsMultiSelection = CreateGroupMultiSelectionDto(appSettingsFile);

        var appSettingsList = groupsMultiSelection
            .SelectMany(i => i.Options);

        if (settings.SelectKeys)
        {
            appSettingsList = DisplayMultiSelection(groupsMultiSelection);
        }

        var strings = CreateListResult(appSettingsFile, appSettingsList, settings.Json);

        DisplayResult(settings.DisplayPerLines, strings);

        return 0;
    }

    private static void DisplayResult(bool displayPerLines, IEnumerable<string> strings)
    {
        if (displayPerLines)
        {
            RepeatableStatus.Run(strings,
                KeysMsg.INF001,
                KeysMsg.INF007,
                KeysMsg.INF006);
        }
        else
        {
            var rows = strings.Select(x => new Text(x));

            AnsiConsole.Write(new Rows(rows));
        }
    }

    private static IEnumerable<string> DisplayMultiSelection(IEnumerable<GroupMultiSelectionDTO> groupsMultiSelection)
    {
        ;
        var multiSelection = new MultiSelectionPrompt<string>()
            .Title("Select [green]keys[/]:")
            .Required()
            .PageSize(10)
            .InstructionsText("[grey](Press [blue]<space>[/] to toggle a database, " +
                              "[green]<enter>[/] to accept)[/]");

        foreach (var group in groupsMultiSelection)
        {
            multiSelection.AddChoiceGroup(group.Name, group.Options);
        }

        return AnsiConsole.Prompt(multiSelection);
    }

    private IEnumerable<GroupMultiSelectionDTO> CreateGroupMultiSelectionDto(List<AppSettingsGroup> appSettings)
    {
        var dto = appSettings
            .Select(x => 
                new GroupMultiSelectionDTO
                    {
                        Name = x.GroupName,
                        Options = x.Keys.Select(appKey => appKey.Key).ToList()
                    });

        return dto;
    }

    private IEnumerable<string> CreateListResult(List<AppSettingsGroup> appSettings, IEnumerable<string> keys, bool json)
    {
        var output = new List<string>();

        foreach (var appSettingsGroup in appSettings)
        {
            foreach (var appKey in appSettingsGroup.Keys)
            {
                if(keys.Contains(appKey.Key, StringComparer.InvariantCultureIgnoreCase))
                    output.Add(!json ? appKey.ToConfig() : appKey.ToJson());
            }
        }

        return output;
    }
}
