using config.Models;
using config.Models.DTOs;
using config.Settings.AppSettings;
using config.Singleton;
using config.Transaction;
using config.Utils;
using config.Utils.Messages;
using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Commands.AppSettings;
internal class UpdateKeysCommand : Command<UpdateKeySettings>
{
    public override int Execute(CommandContext context, UpdateKeySettings settings)
    {
        var appSettings = AppSettingsSingleton.Instance.Lines();

        var groupSelected = GetGroup(appSettings);

        var keySelectedString = GetKeyName(groupSelected);

        SetNewKeyValue(keySelectedString, groupSelected);

        AppSettingsSingleton.Instance.Update(appSettings);

        RepeatableStatus.Run(new RepeatableStatusMsg()
        {
            InitalMsg = KeysMsg.INF001,
            FinalMsg = KeysMsg.INF004,
            RepeatableMsg = KeysMsg.INF005
        }, appSettings.Count);

        return 0;
    }

    private static void SetNewKeyValue(string keySelectedString, AppSettingsGroup groupSelected)
    {
        var newValue = AnsiConsole.Ask<string>($"What will the new key [blue]{keySelectedString}[/] value be?");

        var keySelected = AppSettingsTRA.GetKeyByGroupAndKeyName(groupSelected, keySelectedString);

        keySelected.Value = newValue;
    }

    private static string GetKeyName(AppSettingsGroup groupSelected)
    {
        var keys = AppSettingsTRA.GetOptions(groupSelected);

        var keySelectedString = SelectionDisplay.Selection(keys, "key");
        return keySelectedString;
    }

    private static AppSettingsGroup GetGroup(IEnumerable<AppSettingsGroup> appSettings)
    {
        var groups = AppSettingsTRA.GetGroupsName(appSettings);

        var groupSelectedName = SelectionDisplay.Selection(groups, "group");

        var groupSelected = AppSettingsTRA.GetGroupByName(appSettings, groupSelectedName);

        return groupSelected;
    }
}
