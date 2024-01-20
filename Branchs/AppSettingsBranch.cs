using config.Commands;

using Spectre.Console.Cli;

namespace config.Branchs;
internal static class AppSettingsBranch
{
    public static void UseAppSettingsBranch(this IConfigurator app)
    {
        app.AddBranch("appsettings", app =>
        {
            app.AddCommand<GetKeysCommand>("key");
        });
    }
}
