using config.Commands;
using Spectre.Console.Cli;

namespace config.Branchs
{
    internal static class DatabaseInstancesBranch
	{
		public static void UseDatabasesInstancesBranch(this IConfigurator app)
		{
			app.AddBranch("database", app =>
			{
				app.SetDescription("Insert new database in the list of databases");

				app.AddCommand<CreateDatabaseCommand>("add");
			});
		}
	}
}
  