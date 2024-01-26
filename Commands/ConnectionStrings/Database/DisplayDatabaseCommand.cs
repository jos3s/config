using config.Settings.ConnectionStrings.Database;
using config.Singleton;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Json;
using Spectre.Console.Rendering;
using System.Text.Json;
using config.Models;

namespace config.Commands.ConnectionStrings.Database;
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

    private static void DisplayTable(List<ConnectionLine> databases)
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
                new Text(connectionLine.ConnectionString.InitalCatalog),
                new Text(connectionLine.ConnectionString.Pooling),
                new Text(connectionLine.ConnectionString.ConnectTimeout.ToString()),
                new Text(connectionLine.ConnectionString.AplicationName),
                new Text(connectionLine.ConnectionString.Encrypt)
            });
        }

        AnsiConsole.Write(table);
    }
}
