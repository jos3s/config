using config.Commands.AppSettings;
using Spectre.Console.Cli;

namespace config.Branchs;
internal static class AppSettingsBranch
{
    public static void UseAppSettingsBranch(this IConfigurator app)
    {
        app.AddBranch("appsettings", app =>
        {
            app.SetDescription("Create, Generate or Update values os AppSettings");

            app.AddCommand<GenerateKeysCommand>("generate")
                .WithDescription("Generate list of keys")
                .WithExample("appsettings", "generate")
                .WithExample("appsettings", "generate","-s")
                .WithExample("appsettings", "generate","-j")
                .WithExample("appsettings", "generate","-s","-j");

            app.AddCommand<CreateKeyCommand>("create")
                .WithDescription("Create new key");

            app.AddCommand<UpdateKeysCommand>("update")
                .WithDescription("Update value of key");
        });
    }
}
