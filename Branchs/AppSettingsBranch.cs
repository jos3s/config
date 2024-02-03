using config.Features.AppSettings;
using config.Utils.Messages.Documentation;

using Spectre.Console.Cli;

namespace config.Branchs;
internal static class AppSettingsBranch
{
    public static IConfigurator UseAppSettingsBranch(this IConfigurator app)
    {
        app.AddCommand<GenerateAppSettingsCommand>(DocumentationMsg.COMMAND001)
            .WithDescription(DescriptionMsg.COMMAND001);

        return app;
    }
}
