using config.Features.ConnectionStrings.Settings;
using config.Singleton;
using config.Transaction;
using config.Utils.Display;
using config.Utils.Mapper;
using config.Utils.Messages;
using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Features.ConnectionStrings.Commands
{
    internal class GenerateConnectionStringsCommand : Command<ConnectionStringsSettings>
    {
        public override int Execute(CommandContext context, ConnectionStringsSettings settings)
        {
            try
            {
                var databases = ConnectionStringsSingleton.Instance.Lines();

                var databasesNames = databases.Select(x => x.Name);

                if (settings.SelectDatabases)
                {
                    databasesNames = MultiSelectDisplay.Execute(databases.Select(x => x.Name), "databases");
                }

                var databasesSelected = DatabasesTRA.GetConnectionLinesByNames(databasesNames, databases);

                var output = !settings.JsonFormat
                   ? ConnectionsStringMapper.ToConfig(databasesSelected, settings.User, settings.Password, settings.Instance)
                   : ConnectionsStringMapper.ToJson(databasesSelected, settings.User, settings.Password, settings.Instance);

                if (settings.DisplayStatus)
                {
                    RepeatableStatusDisplay.Run(output.Split(Environment.NewLine).ToList(),
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
