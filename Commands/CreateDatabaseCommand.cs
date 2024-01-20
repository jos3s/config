using config.Messages;
using config.Models;
using config.Settings;
using config.Singleton;
using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Commands
{
    internal class CreateDatabaseCommand : Command<DatabaseSettings>
	{

		public override int Execute(CommandContext context, DatabaseSettings settings)
		{
			try
			{
				AnsiConsole.Status()
					.Start(UpdateDatabasesMsg.INF001, ctx =>
					{

						AnsiConsole.MarkupLine(UpdateDatabasesMsg.INF002);
						Thread.Sleep(1000);

						var connection = new ConnectionLine
						{
							Name = settings.Name,
							ProviderName = settings.ProviderName,
							ConnectionString = new ConnectionString
							{
								AplicationName = settings.AplicationName,
								ConnectTimeout = settings.connectionTimeoutt,
								Encrypt = settings.Encrypt,
								InitalCatalog = settings.Catalog,
								Pooling = settings.Pooling,
							}
						};

						ctx.Status(string.Format(UpdateDatabasesMsg.INF003, "[green]", "[/]"));
						ctx.Spinner(Spinner.Known.Dots);
						ctx.SpinnerStyle(Style.Parse("green"));
						Thread.Sleep(1000);

						ConnectionStringsSingleton.Instance.InsertLine(connection);
						ctx.Status(UpdateDatabasesMsg.INF004);
						ctx.Spinner(Spinner.Known.Dots);
						ctx.SpinnerStyle(Style.Parse("green"));
						Thread.Sleep(2000);

						AnsiConsole.MarkupLine($"[green]{UpdateDatabasesMsg.INF005}[/]");
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
