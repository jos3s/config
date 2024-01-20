using config.Models;
using config.Models.DTOs;
using config.Settings.AppSettings;
using config.Singleton;
using config.Utils;
using config.Utils.Messages;

using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Commands.AppSettings;
internal class GetKeysCommand : Command<GetKeysSettings>
{
    public override int Execute(CommandContext context, GetKeysSettings settings)
    {
        var appSettingsFile = AppSettingsSingleton.Instance.Lines();


        var groupsMultiSelection = CreateGroupMultiSelectionDTO(appSettingsFile);

        var appSettingsList = groupsMultiSelection
            .Select(x => x.Options)
            .SelectMany(i => i);

        if (settings.SelectKeys)
        {
            appSettingsList = MultiSelection(groupsMultiSelection);
        }


        var strings = Lists(appSettingsFile, appSettingsList, settings.Json);

        RepeatableStatus.Run(strings,
            KeysMsg.INF001,
            KeysMsg.INF002,
            KeysMsg.INF003);

        return 0;
    }

    private static IEnumerable<string> MultiSelection(List<GroupMultiSelectionDTO> groupsMultiSelection)
    {
        IEnumerable<string> appSettingsList;
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


        appSettingsList = AnsiConsole.Prompt(multiSelection);
        return appSettingsList;
    }

    private List<GroupMultiSelectionDTO> CreateGroupMultiSelectionDTO(List<AppSettingsGroup> appSettings)
    {
        var dto = new List<GroupMultiSelectionDTO>();

        foreach (var appSettingsGroup in appSettings)
        {
            var group = new GroupMultiSelectionDTO() { Name = appSettingsGroup.GroupName };

            foreach (var appKey in appSettingsGroup.Keys)
            {
                group.Options.Add(appKey.Key);
            }

            dto.Add(group);
        }

        return dto;
    }

    private List<string> Lists(List<AppSettingsGroup> appSettings, IEnumerable<string> keys, bool json)
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
