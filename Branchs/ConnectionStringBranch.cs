using config.Commands.ConnectionStrings;
using config.Commands.ConnectionStrings.Database;
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

                connection.AddCommand<GenerateConnectionStringsCommand>("generate")
                    .WithDescription("Generate connection strings")
                    .WithExample("connection", "generate", "teste.teste", "abcd", "risks")
                    .WithExample("connection", "generate", "teste.teste", "abcd", "risks", "-d")
                    .WithExample("connection", "generate", "teste.teste", "abcd", "risks", "-j", "-d")
                    .WithExample("connection", "generate", "teste.teste", "abcd", "risks", "-d", "--display")
                    .WithExample("connection", "generate", "teste.teste", "abcd", "risks", "-j", "-d", "--display"); 

                connection.AddBranch("database", con =>
                {
                    con.SetDescription("Insert new database in the list of databases");

                    con.AddCommand<CreateDatabaseCommand>("add")
                        .WithDescription("Create new database in the list of databases")
                        .WithExample("connection","database");

                    con.AddCommand<RemoveDatabaseCommand>("remove");
                    con.AddCommand<DisplayDatabaseCommand>("display");
                });
            });
        }
    }
}
