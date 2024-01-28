using config.Features.Settings.Models;
using config.Features.Settings.Settings;
using config.Models;
using config.Models.DTOs;
using config.Singleton;
using config.Transaction;
using config.Utils.Display;
using config.Utils.Messages;
using Spectre.Console.Cli;

namespace config.Features.Settings.Commands;
internal class CreateSettingsCommand : Command<CreateSettingSettings>
{
    public override int Execute(CommandContext context, CreateSettingSettings settings)
    {

        try
        {
            var appSettings = SettingsSingleton.Instance.Lines();

            var newKey = new Setting
            {
                Key = settings.KeyName,
                Value = settings.KeyValue
            };

            var group = SettingsTRA.GetGroupByName(appSettings, settings.GroupName);

            if (group == null)
            {
                var newGroup = new SettingsGroup
                {
                    GroupName = settings.GroupName,
                    Keys = new List<Setting> { newKey },
                };
                appSettings.Add(newGroup);
            }
            else
            {
                group.Keys.Add(newKey);
            }

            SettingsSingleton.Instance.Update(appSettings);

            RepeatableStatusDisplay.Run(new RepeatableStatusMsg
            {
                InitalMsg = SettingsMsg.INF001,
                FinalMsg = SettingsMsg.INF003,
                RepeatableMsg = SettingsMsg.INF002
            }, 2);

            return 0;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
