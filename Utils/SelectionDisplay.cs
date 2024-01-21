using config.Models.DTOs;

using Spectre.Console;

namespace config.Utils;
internal class SelectionDisplay
{
    public static string Selection(IEnumerable<GroupSelectionDTO> groupsSelection, string nameOfOptions, int pageSize = 10)
    {

        var selectionPrompt = new SelectionPrompt<string>()
            .Title($"Select [green]{nameOfOptions}[/]:")
            .PageSize(pageSize)
            .MoreChoicesText($"[grey](Move up and down to reveal more {nameOfOptions})[/]");

        foreach (var group in groupsSelection)
        {
            selectionPrompt.AddChoiceGroup(group.Name, group.Options);
        }

        return AnsiConsole.Prompt(selectionPrompt);
    }
    
    public static string Selection(GroupSelectionDTO groupSelection, string nameOfOptions, int pageSize = 10)
    {

        var selectionPrompt = new SelectionPrompt<string>()
            .Title($"Select [green]{nameOfOptions}[/]:")
            .PageSize(pageSize)
            .MoreChoicesText($"[grey](Move up and down to reveal more {nameOfOptions})[/]")
            .AddChoices(groupSelection.Options);
       
        return AnsiConsole.Prompt(selectionPrompt);
    }

    public static string Selection(IEnumerable<string> options, string nameOfOptions, int pageSize = 10)
    {

        var selectionPrompt = new SelectionPrompt<string>()
            .Title($"Select [green]{nameOfOptions}[/]:")
            .PageSize(pageSize)
            .MoreChoicesText($"[grey](Move up and down to reveal more {nameOfOptions})[/]")
            .AddChoices(options);
       
        return AnsiConsole.Prompt(selectionPrompt);
    }
}
