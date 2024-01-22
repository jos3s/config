using config.Models;
using config.Models.DTOs;
using config.Settings.AppSettings;
using config.Singleton;
using config.Transaction;
using config.Utils.Display;
using config.Utils.Messages;
using Spectre.Console.Cli;

namespace config.Commands.AppSettings;
internal class CreateKeyCommand : Command<CreateKeySettings>
{
    public override int Execute(CommandContext context, CreateKeySettings settings)
    {

        try
        {
            var appSettings = AppSettingsSingleton.Instance.Lines();

            var newKey = new AppKey
            {
                Key = settings.KeyName,
                Value = settings.KeyValue
            };

            var group = AppSettingsTRA.GetGroupByName(appSettings, settings.GroupName);
            
            if (group == null)
            {
                var newGroup = new AppSettingsGroup
                {
                    GroupName = settings.GroupName,
                    Keys = new List<AppKey> { newKey },
                };
                appSettings.Add(newGroup);
            }
            else
            {
                group.Keys.Add(newKey);
            }

            AppSettingsSingleton.Instance.Update(appSettings);

            RepeatableStatusDisplay.Run(new RepeatableStatusMsg
            {
                InitalMsg = KeysMsg.INF001,
                FinalMsg = KeysMsg.INF003,
                RepeatableMsg = KeysMsg.INF002
            } ,2);

            return 0;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
