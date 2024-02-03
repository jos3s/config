using config.DTOs;
using config.Singleton;
using config.Transaction;
using config.Utils.Display;
using config.Utils.Messages;

using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Features.Settings.Remove;
internal class RemoveSettingsCommand : Command<RemoveSettingSettings>
{
    public override int Execute(CommandContext context, RemoveSettingSettings settings)
    {

        var appSettings = SettingsSingleton.Instance.Lines();

        var groups = SettingsTRA.GetGroupsName(appSettings);

        var groupSelectedName = SelectionDisplay.Selection(groups, "group");

        var groupSelected = SettingsTRA.GetGroupByName(appSettings, groupSelectedName);

        var keys = SettingsTRA.GetOptions(groupSelected);

        var keySelectedString = SelectionDisplay.Selection(keys, "key");

        var keySelected = SettingsTRA.GetKeyByGroupAndKeyName(groupSelected, keySelectedString);

        if (AnsiConsole.Confirm($"Remove {keySelectedString} key?", false))
        {
            groupSelected.Keys.Remove(keySelected);
            SettingsSingleton.Instance.Update(appSettings);
            RepeatableStatusDisplay.Run(new RepeatableStatusMsg
            {
                InitalMsg = SettingsMsg.INF001,
                RepeatableMsg = SettingsMsg.INF009,
                FinalMsg = SettingsMsg.INF008
            }, 2);
        }
        else
        {
            AnsiConsole.WriteLine("Key not removed.");
        }

        return 0;
    }
}
