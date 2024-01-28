using config.Features.ConnectionStrings;
using config.Utils.Messages.Documentation;
using Spectre.Console.Cli;

namespace config.Branchs
{
    internal static class ConnectionStringsBranch
    {
        public static IConfigurator UseConnectionStringsBranch(this IConfigurator app)
        {
            app.AddCommand<GenerateConnectionStringsCommand>(DocumentationMsg.COMMAND002)
                .WithDescription(DescriptionMsg.COMMAND002);

            return app;
        }
    }
}
