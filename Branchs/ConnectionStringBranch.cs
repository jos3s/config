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

                connection.AddBranch<ConnectionSettings>("generate",con =>
                {
                    con.SetDescription("Generate connection strings");

                    con.AddCommand<ConfigConnectionStringsCommand>("config")
                        .WithDescription("Create connection strings for config files")
                        .WithExample("connection", "generate", "teste.teste", "abcd", "risks", "config")
                        .WithExample("connection", "generate", "teste.teste", "abcd", "risks", "config", "-d")
                        .WithExample("connection", "generate", "teste.teste", "abcd", "risks", "config", "-d","--display");

                    con.AddCommand<JsonConnectionStringsCommand>("json")
                        .WithDescription("Create connection strings for json files")
                        .WithExample("connection", "generate", "teste.teste", "abcd", "risks", "json")
                        .WithExample("connection", "generate", "teste.teste", "abcd", "risks", "json", "-d")
                        .WithExample("connection", "generate", "teste.teste", "abcd", "risks", "json", "--display", "-d");
                });

                connection.AddBranch("database", con =>
                {
                    con.SetDescription("Insert new database in the list of databases");

                    con.AddCommand<CreateDatabaseCommand>("add")
                        .WithDescription("Create new database in the list of databases")
                        .WithExample("connection","database");
                });
            });
        }
    }
}
