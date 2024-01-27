using config.Commands.AppSettings;

using Spectre.Console.Cli;

namespace config.Branchs;
internal static class AppSettingsBranch
{
    public static IConfigurator UseAppSettingsBranch(this IConfigurator app)
    {
        app.AddCommand<GenerateAppSettingsCommand>("appsettings")
            .WithDescription("Generate list of keys")
            .WithExample("appsettings", "generate")
            .WithExample("appsettings", "generate", "-s")
            .WithExample("appsettings", "generate", "-j")
            .WithExample("appsettings", "generate", "-s", "-j");

        return app;
    }
}
