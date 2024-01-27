using config.Singleton;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Json;
using Spectre.Console.Rendering;
using System.Text.Json;
using config.Models;
using config.Settings.Database;

namespace config.Commands.Database;
internal class DisplayDatabaseCommand : Command<DisplayDatabaseSettings>
{
    public override int Execute(CommandContext context, DisplayDatabaseSettings settings)
    {
        var databases = ConnectionStringsSingleton.Instance.Lines();

        if (settings.DisplayInJson)
        {
            var databaseJson = new JsonText(JsonSerializer.Serialize(databases));

            AnsiConsole.Write(databaseJson);
            return 0;
        }

        DisplayTable(databases);

        return 0;
    }

    private static void DisplayTable(List<Models.Database> databases)
    {
        var table = new Table();
        table.AddColumn("Name");
        table.AddColumn("ProviderName");
        table.AddColumn("InitalCatalog");
        table.AddColumn("Pooling");
        table.AddColumn("ConnectTimeout");
        table.AddColumn("AplicationName");
        table.AddColumn("Encrypt");


        foreach (var connectionLine in databases)
        {
            table.AddRow(new List<IRenderable>()
            {
                new Text(connectionLine.Name),
                new Text(connectionLine.ProviderName),
                new Text(connectionLine.InitalCatalog),
                new Text(connectionLine.Pooling),
                new Text(connectionLine.ConnectTimeout.ToString()),
                new Text(connectionLine.AplicationName),
                new Text(connectionLine.Encrypt)
            });
        }

        AnsiConsole.Write(table);
    }
}
