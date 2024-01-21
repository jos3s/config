using config.Settings.ConnectionStrings;
using config.Singleton;
using config.Transaction;
using config.Utils;
using config.Utils.Messages;
using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Commands.ConnectionStrings
{
    internal class GenerateConnectionStringsCommand : Command<ConnectionSettings>
    {
        public override int Execute(CommandContext context, ConnectionSettings settings)
        {
            try
            {
                var databases = ConnectionStringsSingleton.Instance.Lines();

                var databasesNames = databases.Select(x => x.Name);

                if (settings.SelectDatabases)
                {
                    databasesNames = MultiSelectDisplay.Execute(databases.Select(x => x.Name), "databases");
                }

                var databasesSelected = ConnectionLinesTRA.GetConnectionLinesByNames(databasesNames, databases);

                 var output = !settings.JsonFormat 
                    ? CreateLine.Config(databasesSelected, settings.User, settings.Password, settings.Instance) 
                    : CreateLine.Json(databasesSelected, settings.User, settings.Password, settings.Instance);

                if (settings.DisplayStatus)
                {
                    RepeatableStatus.Run(output.Split(Environment.NewLine).ToList(),
                        ConnectionStringsMsg.INF001,
                        ConnectionStringsMsg.INF002,
                        ConnectionStringsMsg.INF003
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
}
