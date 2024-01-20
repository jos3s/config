using System.Reflection;
using System.Text.RegularExpressions;
using config.Models;
using config.Models.DTOs;
using config.Settings;
using config.Singleton;
using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Commands;
internal class GetKeysCommand : Command<AppSettingsSettings>
{
    public override int Execute(CommandContext context, AppSettingsSettings settings)
    {
        var lines = AppSettingsSingleton.Instance.Lines();

        var groups = CreateGroupMultiSelectionDTO(lines);

        var multiSelection = new MultiSelectionPrompt<string>()
            .Title("Select [green]keys[/]:")
            .Required()
            .PageSize(10)
            .InstructionsText("[grey](Press [blue]<space>[/] to toggle a database, " +
                              "[green]<enter>[/] to accept)[/]");

        foreach (var group in groups)
        {
            multiSelection.AddChoiceGroup(group.Name, group.Options);
        }

        var dataBases = AnsiConsole.Prompt(multiSelection);
        Lists(lines, dataBases);

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


    private void Lists(object appSettings, List<string> keys)
    {
        foreach (PropertyInfo propertyInfo in appSettings.GetType().GetProperties())
        {

            var propertsObject = propertyInfo.GetValue(appSettings, null);

            var properts = (List<AppKey>)propertsObject;

            foreach (var propert in properts)
            {
                if (keys.Contains(propert.Key))
                {
                    AnsiConsole.WriteLine(propert.ToString());
                }
            }

        }
    }
}
