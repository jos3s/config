using config.DTOs;
using config.Features.Settings.Shared;
using config.Singleton;
using config.Transaction;
using config.Utils.Display;
using config.Utils.Messages;

using Spectre.Console.Cli;

namespace config.Features.Settings.Create;
internal class CreateSettingsCommand : Command<CreateSettingSettings>
{
    public override int Execute(CommandContext context, CreateSettingSettings settings)
    {

        try
        {
            var appSettings = SettingsSingleton.Instance.Lines();

            var newKey = new SettingModel
            {
                Key = settings.KeyName,
                Value = settings.KeyValue
            };

            var group = SettingsTRA.GetGroupByName(appSettings, settings.GroupName);

            if (group == null)
            {
                var newGroup = new SettingsGroupModel
                {
                    GroupName = settings.GroupName,
                    Keys = new List<SettingModel> { newKey },
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
