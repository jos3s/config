using config.Commands.ConnectionStrings;
using config.Settings.ConnectionStrings;
using Spectre.Console.Cli;

namespace config.Branchs
{
    internal static class ConnectionStringsBranch 
	{
		public static void UseConnectionStringsBranch(this IConfigurator app)
		{
            app.AddBranch("connection", connection =>
			{
                connection.SetDescription("Create connection strings or update list of databases");

				create.AddBranch<UserSettings>("user", user =>
				{
					user.SetDescription("Set user of connection strings");

					user.AddBranch<PasswordSettings>("pass", password =>
					{
						password.SetDescription("Set password of connection strings");

                connection.AddBranch("database", con =>
						{
                    con.SetDescription("Insert new database in the list of databases");

                    con.AddCommand<CreateDatabaseCommand>("add")
                        .WithDescription("Create new database in the list of databases")
                        .WithExample("connection","database");
                });
						})
						.WithAlias("i");
					})
					.WithAlias("p");
				})
				.WithAlias("u");
			});
		}
	}
}
