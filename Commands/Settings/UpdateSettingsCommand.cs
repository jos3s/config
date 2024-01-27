using config.Models;
using config.Models.DTOs;
using config.Settings.Settings;
using config.Singleton;
using config.Transaction;
using config.Utils.Display;
using config.Utils.Messages;
using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Commands.Settings;
internal class UpdateSettingsCommand : Command<UpdateSettingSettings>
{
    public override int Execute(CommandContext context, UpdateSettingSettings settings)
    {
        var appSettings = SettingsSingleton.Instance.Lines();

        var groupSelected = GetGroup(appSettings);

        var keySelectedString = GetKeyName(groupSelected);

        SetNewKeyValue(keySelectedString, groupSelected);

        SettingsSingleton.Instance.Update(appSettings);

        RepeatableStatusDisplay.Run(new RepeatableStatusMsg()
        {
            InitalMsg = SettingsMsg.INF001,
            FinalMsg = SettingsMsg.INF004,
            RepeatableMsg = SettingsMsg.INF005
        }, appSettings.Count);

        return 0;
    }

    private static void SetNewKeyValue(string keySelectedString, SettingsGroup groupSelected)
    {
        var newValue = AnsiConsole.Ask<string>($"What will the new key [blue]{keySelectedString}[/] value be?");

        var keySelected = SettingsTRA.GetKeyByGroupAndKeyName(groupSelected, keySelectedString);

        keySelected.Value = newValue;
    }

    private static string GetKeyName(SettingsGroup groupSelected)
    {
        var keys = SettingsTRA.GetOptions(groupSelected);

        var keySelectedString = SelectionDisplay.Selection(keys, "key");
        return keySelectedString;
    }

    private static SettingsGroup GetGroup(IEnumerable<SettingsGroup> appSettings)
    {
        var groups = SettingsTRA.GetGroupsName(appSettings);

        var groupSelectedName = SelectionDisplay.Selection(groups, "group");

        var groupSelected = SettingsTRA.GetGroupByName(appSettings, groupSelectedName);

        return groupSelected;
    }
}
