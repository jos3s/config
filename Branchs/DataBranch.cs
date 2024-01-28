using config.Commands.Database;
using config.Commands.Settings;
using config.Utils.Messages.Documentation;

using Spectre.Console.Cli;

namespace config.Branchs;
internal static class DataBranch
{
    public static IConfigurator UseDataBranch(this IConfigurator app)
    {
        app.AddBranch(DocumentationMsg.BRANCH001, data =>
        {
            data.SetDescription(DescriptionMsg.BRANCH001);

            data.AddBranch(DocumentationMsg.BRANCH002, database =>
            {
                database.SetDescription(DescriptionMsg.BRANCH002);

                database.AddCommand<CreateDatabaseCommand>(DocumentationMsg.COMMAND003)
                    .WithDescription(string.Format(DescriptionMsg.COMMAND003, "database"));

                database.AddCommand<RemoveDatabaseCommand>(DocumentationMsg.COMMAND004)
                    .WithDescription(string.Format(DescriptionMsg.COMMAND004, "database"));

                database.AddCommand<DisplayDatabaseCommand>(DocumentationMsg.COMMAND006)
                    .WithDescription(string.Format(DescriptionMsg.COMMAND006, "all databases"))
                    .WithExample(DocumentationMsg.BRANCH001, DocumentationMsg.BRANCH002, DocumentationMsg.COMMAND006)
                    .WithExample(DocumentationMsg.BRANCH001, DocumentationMsg.BRANCH002, DocumentationMsg.COMMAND006, "-j"); 
            });

            data.AddBranch(DocumentationMsg.BRANCH003, settings =>
            {
                settings.SetDescription(DescriptionMsg.BRANCH003);

                settings.AddCommand<CreateSettingsCommand>(DocumentationMsg.COMMAND003)
                    .WithDescription(string.Format(DescriptionMsg.COMMAND003, "setting key"));

                settings.AddCommand<UpdateSettingsCommand>(DocumentationMsg.COMMAND005)
                    .WithDescription(string.Format(DescriptionMsg.COMMAND005, "setting key"));

                settings.AddCommand<RemoveSettingsCommand>(DocumentationMsg.COMMAND004)
                    .WithDescription(string.Format(DescriptionMsg.COMMAND004, "setting key"));

                settings.AddCommand<DisplaySettingsCommand>(DocumentationMsg.COMMAND006)
                    .WithDescription(string.Format(DescriptionMsg.COMMAND006, "all settings keys"))
                    .WithExample(DocumentationMsg.BRANCH001, DocumentationMsg.BRANCH003, DocumentationMsg.COMMAND006)
                    .WithExample(DocumentationMsg.BRANCH001, DocumentationMsg.BRANCH003, DocumentationMsg.COMMAND006, "-j");
            });
        });

        return app;
    }
}
