using config.Settings.ConnectionStrings;
using config.Singleton;
using config.Utils;

using Spectre.Console;
using Spectre.Console.Cli;

internal class JsonConnectionStringsCommand : Command<OptionConnectionStringsSettings>
{
	public override int Execute(CommandContext context, OptionConnectionStringsSettings settings)
	{
		try
		{
			var list = ConnectionStringsSingleton.Instance.Lines();
			if ((bool)settings.SelectDatabases)
			{
				var databases = MultiSelect.Execute();

				if (databases?.Count > 0)
					list = ConnectionStringsSingleton.Instance.Lines().Where(connection => databases.Contains(connection.Name)).ToList();
			}

            var output = CreateLine.Json(list, settings.User, settings.Password, settings.Instance);

            if (settings.DisplayStatus)
            {
                RepeatableStatus.Run(output.Split(Environment.NewLine).ToList(),
                    "Initializing connection strings...",
                    "Creating new connection string...",
                    "Success, all connection strings created."
                );
            }
            else
            {
                AnsiConsole.MarkupLine(output);
            }

            return 0;
		}
		catch (Exception)
		{
			throw;
		}
	}
}