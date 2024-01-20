using config.Singleton;
using Spectre.Console;

namespace config.Utils
{
	public class DatabaseMultiSelect
	{
		public static List<string> Execute()
		{
			try
			{
				var listDatabaseNames = ConnectionStringsSingleton.Instance.Lines().Select(x => x.Name).ToArray();

				var dataBases = AnsiConsole.Prompt(
				new MultiSelectionPrompt<string>()
					.Title("Select [green]databases[/]:")
					.Required()
					.PageSize(3)
					.InstructionsText("[grey](Press [blue]<space>[/] to toggle a database, " +
						"[green]<enter>[/] to accept)[/]")
					.AddChoiceGroup("All", listDatabaseNames));

				return dataBases;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
