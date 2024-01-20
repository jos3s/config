using config.Models;
using config.Models.DTOs;
using config.Settings.AppSettings;
using config.Singleton;
using config.Utils;
using config.Utils.Messages;

using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Commands.AppSettings;
internal class UpdateKeysCommand : Command<UpdateKeySettings>
{
    public override int Execute(CommandContext context, UpdateKeySettings settings)
    {
        var appSettingsFile = AppSettingsSingleton.Instance.Lines();

        var groupsMultiSelection = CreateGroupMultiSelectionDTO(appSettingsFile);

        var keySelected = Selection(groupsMultiSelection);

        var newValue = AnsiConsole.Ask<string>($"What will the new key [blue]{keySelected}[/] value be?");


        foreach (var group in appSettingsFile)
        {
            foreach (var key in group.Keys)
            {
                if (key.Key.Equals(keySelected, StringComparison.InvariantCultureIgnoreCase))
                    key.Value = newValue;
            }
        }

        AppSettingsSingleton.Instance.Update(appSettingsFile);


        RepeatableStatus.Run(new RepeatableStatusMsg()
        {
            InitalMsg = KeysMsg.INF001,
            FinalMsg = KeysMsg.INF004,
            RepeatableMsg = KeysMsg.INF005
        }, appSettingsFile.Count);

        return 0;
    }

    private static string Selection(List<GroupMultiSelectionDTO> groupsMultiSelection)
    {

        var multiSelection = new SelectionPrompt<string>()
            .Title("Select [green]keys[/]:")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more keys)[/]");

        foreach (var group in groupsMultiSelection)
        {
            multiSelection.AddChoiceGroup(group.Name, group.Options);
        }


        return AnsiConsole.Prompt(multiSelection);
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

}
