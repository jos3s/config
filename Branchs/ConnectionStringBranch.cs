using config.Commands.ConnectionStrings;
using Spectre.Console.Cli;

namespace config.Branchs
{
    internal static class ConnectionStringsBranch
    {
        public static IConfigurator UseConnectionStringsBranch(this IConfigurator app)
        {
            app.AddCommand<GenerateConnectionStringsCommand>("connection")
                .WithDescription("Generate connection strings");

            return app;
        }
    }
}
