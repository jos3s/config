using config.Features.Database.Settings;
using config.Models.DTOs;
using config.Singleton;
using config.Utils.Display;
using config.Utils.Messages;
using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Features.Database.Commands;
internal class RemoveDatabaseCommand : Command<RemoveDatabaseSettings>
{
    public override int Execute(CommandContext context, RemoveDatabaseSettings settings)
    {
        var databases = ConnectionStringsSingleton.Instance.Lines();

        if (databases.Count == 0)
        {
            AnsiConsole.Write("Not databases to remove.");
            return 0;
        }

        var databasesNames = databases.Select(x => x.Name);

        var databaseSelected = SelectionDisplay.Selection(databasesNames, "database");

        if (AnsiConsole.Confirm($"Remove {databaseSelected} database?", false))
        {
            var databaseSelectedObject = databases.FirstOrDefault(x => x.Name.Equals(databaseSelected, StringComparison.CurrentCultureIgnoreCase));

            databases.Remove(databaseSelectedObject);

            ConnectionStringsSingleton.Instance.Update(databases);

            RepeatableStatusDisplay.Run(new RepeatableStatusMsg
            {
                InitalMsg = DatabasesMsg.INF006,
                RepeatableMsg = DatabasesMsg.INF007,
                FinalMsg = DatabasesMsg.INF008
            }, 2);

        }
        else
        {
            AnsiConsole.Write("Database not removed.");
        }

        return 0;
    }
}
