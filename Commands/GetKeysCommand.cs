using System.Reflection;
using System.Text.RegularExpressions;
using config.Models;
using config.Models.DTOs;
using config.Settings;
using config.Singleton;
using config.Utils;
using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Commands;
internal class GetKeysCommand : Command<AppSettingsSettings>
{
    public override int Execute(CommandContext context, AppSettingsSettings settings)
    {
        var appSettingsFile = AppSettingsSingleton.Instance.Lines();


        var groupsMultiSelection = CreateGroupMultiSelectionDTO(appSettingsFile);

        var appSettingsList = groupsMultiSelection
            .Select(x=> x.Options)
            .SelectMany(i => i);

        if (settings.SelectKeys)
        {
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
        }


        var strings = Lists(appSettingsFile, appSettingsList, settings.Json);

        RepeatableStatus.Run(strings,
            "Collecting app keys...",
            "Creating new key...",
            "App keys successfully created...");

        return 0;
    }

    private List<GroupMultiSelectionDTO> CreateGroupMultiSelectionDTO(object appSettings)
    {
        var dto = new List<GroupMultiSelectionDTO>();
        foreach (PropertyInfo propertyInfo in appSettings.GetType().GetProperties())
        {
            var group = new GroupMultiSelectionDTO(){ Name = propertyInfo.Name };

            var propertsObject = propertyInfo.GetValue(appSettings, null) ;

            var properts = (List<AppKey>)propertsObject;

            foreach (var propert in properts)
            {
                group.Options.Add(propert.Key);
            }

            dto.Add(group);
        }

        return dto;
    }

    private List<string> Lists(object appSettings, IEnumerable<string> keys, bool json)
    {
        var output = new List<string>();

        foreach (PropertyInfo propertyInfo in appSettings.GetType().GetProperties())
        {

            var propertsObject = propertyInfo.GetValue(appSettings, null);

            var properts = (List<AppKey>)propertsObject;

            foreach (var propert in properts)
            {
                if (keys.Contains(propert.Key))
                {
                    output.Add(!json ? propert.ToConfig() : propert.ToJson());
                }
            }

        }

        return output;
    }
}
