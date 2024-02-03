using config.Features.Settings.Shared;

using Spectre.Console;

namespace config.Utils.Display;

internal class MultiSelectDisplay
{
    public static IEnumerable<string> Execute(IEnumerable<string> choices, string nameOfOptions, int pageSize = 10)
    {
        return AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title($"Select [green]{nameOfOptions}[/]:")
                .Required()
                .PageSize(pageSize)
                .InstructionsText($"[grey](Press [blue]<space>[/] to toggle a {nameOfOptions}, " +
                                  "[green]<enter>[/] to accept)[/]")
                .AddChoiceGroup("All", choices));
    }

    public static IEnumerable<string> ExecuteForSettingsGroup(IEnumerable<SettingsGroupModel> appSettings, string nameOfOptions, int pageSize = 10)
    {
        var multiSelection = new MultiSelectionPrompt<string>()
            .Title($"Select [green]{nameOfOptions}[/]:")
            .Required()
            .PageSize(appSettings.Count() < 3 ? pageSize : appSettings.Count())
            .InstructionsText($"[grey](Press [blue]<space>[/] to toggle a {nameOfOptions}, " +
                              "[green]<enter>[/] to accept)[/]");

        foreach (var group in appSettings)
        {
            multiSelection.AddChoiceGroup(group.GroupName, group.Keys.Select(x => x.Key));
        }

        return AnsiConsole.Prompt(multiSelection);
    }
}
