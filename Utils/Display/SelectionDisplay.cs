using config.Models.DTOs;

using Spectre.Console;

namespace config.Utils.Display;
internal class SelectionDisplay
{
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
