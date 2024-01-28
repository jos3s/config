using config.Features.Database.Shared;
using config.Singleton;
using config.Utils.Messages;

using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Features.Database.Create
{
    internal class CreateDatabaseCommand : Command<CreateDatabaseSettings>
    {
        public override int Execute(CommandContext context, CreateDatabaseSettings settings)
        {
            try
            {
                AnsiConsole.Status()
                    .Start(DatabasesMsg.INF001, ctx =>
                    {

                        AnsiConsole.MarkupLine(DatabasesMsg.INF002);
                        Thread.Sleep(1000);

                        var connection = new DatabaseModel
                        {
                            Name = settings.Name,
                            ProviderName = settings.ProviderName,
                            AplicationName = settings.AplicationName,
                            ConnectTimeout = settings.connectionTimeoutt,
                            Encrypt = settings.Encrypt,
                            InitalCatalog = settings.Catalog,
                            Pooling = settings.Pooling,
                        };

                        ctx.Status(string.Format(DatabasesMsg.INF003, "[green]", "[/]"));
                        ctx.Spinner(Spinner.Known.Dots);
                        ctx.SpinnerStyle(Style.Parse("green"));
                        Thread.Sleep(1000);

                        ConnectionStringsSingleton.Instance.InsertLine(connection);
                        ctx.Status(DatabasesMsg.INF004);
                        ctx.Spinner(Spinner.Known.Dots);
                        ctx.SpinnerStyle(Style.Parse("green"));
                        Thread.Sleep(2000);

                        AnsiConsole.MarkupLine($"[green]{DatabasesMsg.INF005}[/]");
                    });

                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
