using config.Features.Database.Commands;
using config.Features.Settings.Commands;
using Spectre.Console.Cli;

namespace config.Branchs;
internal static class DataBranch 
{
    public static IConfigurator UseDatabaseBranch(this IConfigurator app)
    {
        app.AddBranch("data", app =>
        {
            app.AddBranch("database", database =>
            {
                database.SetDescription("Create, remove or display databases in the list of databases");

                database.AddCommand<CreateDatabaseCommand>("create")
                    .WithDescription("Create new database in the list of databases");

                database.AddCommand<RemoveDatabaseCommand>("remove");

                database.AddCommand<DisplayDatabaseCommand>("display");
            });

            app.AddBranch("settings", settings =>
            {
                settings.SetDescription("Create, update, remove or display settings in the list of settings");
                settings.AddCommand<CreateSettingsCommand>("create")
                    .WithDescription("Create new key");

                settings.AddCommand<UpdateSettingsCommand>("update")
                    .WithDescription("Update value of key");

                settings.AddCommand<RemoveSettingsCommand>("remove")
                    .WithDescription("Remove a key");

                settings.AddCommand<DisplaySettingsCommand>("display")
                    .WithDescription("Display all keys");
            });
        });

        return app;
    }
}
