﻿using config.DTOs;
﻿using config.Singleton;
using config.Transaction;
using config.Utils.Display;
using config.Utils.Extensions;
using config.Utils.Mapper;
using config.Utils.Messages;

using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Features.ConnectionStrings;

internal class GenerateConnectionStringsCommand : Command<ConnectionStringsSettings>
{
    public override int Execute(CommandContext context, ConnectionStringsSettings settings)
    {
        try
        {
            var databases = ConnectionStringsSingleton.Instance.Lines();

            var databasesNames = databases.Select(x => x.Name);

            if (settings.SelectDatabases)
            {
                databasesNames = MultiSelectDisplay.Execute(databases.Select(x => x.Name), "databases");
            }

            var databasesSelected = DatabasesTRA.GetConnectionLinesByNames(databasesNames, databases);

            var output = !settings.JsonFormat
               ? ConnectionsStringMapper.ToConfig(databasesSelected, settings.User, settings.Password, settings.Instance)
               : ConnectionsStringMapper.ToJson(databasesSelected, settings.User, settings.Password, settings.Instance);
            var connectionInfo = new ConnectionInfoDTO(settings.Instance, settings.User, settings.Password);

            if (!string.IsNullOrEmpty(settings.ExportPath) || !string.IsNullOrWhiteSpace(settings.ExportPath))
            {
                var lines = !settings.JsonFormat
                   ? ConnectionsStringMapper.ToConfig(databasesSelected, settings.User, settings.Password, settings.Instance, toFile: true)
                   : ConnectionsStringMapper.ToJson(databasesSelected, settings.User, settings.Password, settings.Instance, toFile: true);

                CreateExportFile(settings.ExportPath, lines);
            }

            if (settings.Display)
            {
                AnsiConsole.MarkupLine(output);
            }

            return 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static void CreateExportFile(string exportPath, string text)
    {
        CreateFileTRA.ValidatePath(exportPath);

        exportPath += @"\connectionstrings.txt";

        AnsiConsole.Status()
            .Start(FileMsg.INF005, ctx =>
            {
                // Simulate some work
                ctx.Status(FileMsg.INF001);
                ctx.Spinner(Spinner.Known.Balloon);
                ctx.SpinnerStyle(Style.Parse("green"));
                Thread.Sleep(1000);

                // Update the status and spinner
                ctx.Status(FileMsg.INF002);
                ctx.Spinner(Spinner.Known.Balloon);
                ctx.SpinnerStyle(Style.Parse("green"));
                Thread.Sleep(1000);


                ctx.Status(FileMsg.INF003);
                ctx.Spinner(Spinner.Known.Balloon);
                ctx.SpinnerStyle(Style.Parse("green"));
                ctx.Refresh();
                Thread.Sleep(1000);


                text = text.Replace("[]", "").Replace("[/]", "");
                CreateFileTRA.WriteInFile(exportPath, text);

                var panel = new Panel(
                string.Format(FileMsg.INF004, $"[blue]{exportPath}[/]"))
                    .Formatted();

                Thread.Sleep(1000);
                AnsiConsole.Write(panel);
            });
    }
}
