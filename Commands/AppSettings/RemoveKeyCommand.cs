using config.Models.DTOs;
using config.Settings.AppSettings;
using config.Singleton;
using config.Transaction;
using config.Utils.Display;
using config.Utils.Messages;
using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Commands.AppSettings;
internal class RemoveKeyCommand : Command<RemoveKeySettings>
{
    public override int Execute(CommandContext context, RemoveKeySettings settings)
    {

        var appSettings = AppSettingsSingleton.Instance.Lines();

        var groups = AppSettingsTRA.GetGroupsName(appSettings);

        var groupSelectedName = SelectionDisplay.Selection(groups, "group");

        var groupSelected = AppSettingsTRA.GetGroupByName(appSettings, groupSelectedName);

        var keys = AppSettingsTRA.GetOptions(groupSelected);

        var keySelectedString = SelectionDisplay.Selection(keys, "key");

        var keySelected = AppSettingsTRA.GetKeyByGroupAndKeyName(groupSelected, keySelectedString);

        if (AnsiConsole.Confirm($"Remove {keySelectedString} key?", false))
        {
            groupSelected.Keys.Remove(keySelected);
            AppSettingsSingleton.Instance.Update(appSettings);
            RepeatableStatusDisplay.Run(new RepeatableStatusMsg
            {
                InitalMsg = KeysMsg.INF001,
                RepeatableMsg = KeysMsg.INF009,
                FinalMsg = KeysMsg.INF008
            }, 2);
        }
        else
        {
            AnsiConsole.WriteLine("Key not removed.");
        }

        return 0;
    }
}
