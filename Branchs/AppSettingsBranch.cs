using config.Commands;

using Spectre.Console.Cli;

namespace config.Branchs;
internal static class AppSettingsBranch
{
    public static void UseAppSettingsBranch(this IConfigurator app)
    {
        app.AddBranch("appsettings", app =>
        {
            app.SetDescription("Create, Read, Update values os AppSettings");

            app.AddCommand<GetKeysCommand>("generate")
                .WithDescription("Generate list of keys")
                .WithExample("appsettings", "generate")
                .WithExample("appsettings", "generate","-s")
                .WithExample("appsettings", "generate","-j")
                .WithExample("appsettings", "generate","-s","-j");
        });
    }
}
