using config.Commands.ConnectionStrings;
using config.Settings.ConnectionStrings;
using Spectre.Console.Cli;

namespace config.Branchs
{
    internal static class ConnectionStringsBranch 
	{
		public static void UseConnectionStringsBranch(this IConfigurator app)
		{
			app.AddBranch("connection", create =>
			{
				create.SetDescription("Create connection strings");
				create.AddExample("create","user", "teste.teste", "pass", "abcd", "inst", "risks");
				create.AddExample("create","user", "teste.teste", "pass", "abcd", "inst", "risks", "-d");

				create.AddBranch<UserSettings>("user", user =>
				{
					user.SetDescription("Set user of connection strings");

					user.AddBranch<PasswordSettings>("pass", password =>
					{
						password.SetDescription("Set password of connection strings");

						password.AddBranch<InstanceSettings>("inst", instance =>
						{
							instance.SetDescription("Set instance of connection strings");

							instance.AddCommand<ConfigConnectionStringsCommand>("config")
								.WithDescription("Create connection strings for config files");
							instance.AddCommand<JsonConnectionStringsCommand>("json")
								.WithDescription("Create connection strings for json files");
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
