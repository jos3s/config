using Spectre.Console;

namespace config.Utils
{
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
    }
}
