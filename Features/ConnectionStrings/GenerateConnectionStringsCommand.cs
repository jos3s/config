using config.DTOs;
using config.Features.Database.Shared;
using config.Singleton;
using config.Transaction;
using config.Utils.Display;
using config.Utils.Extensions;
using config.Utils.Mapper;
using config.Utils.Messages;

using Spectre.Console;
using Spectre.Console.Cli;

using System.Text;
using System.Text.RegularExpressions;

namespace config.Features.ConnectionStrings;

internal class GenerateConnectionStringsCommand : Command<ConnectionStringsSettings>
{
    public override int Execute(CommandContext context, ConnectionStringsSettings settings)
    {
        try
        {
            IEnumerable<DatabaseModel> databasesSelected = selectDatabases(settings);

            Func<IEnumerable<DatabaseModel>, ConnectionInfoDTO, bool, List<string>> toLines = !settings.Json
                ? ConnectionsStringMapper.ToConfigList
                : ConnectionsStringMapper.ToJsonList;

            var connectionInfo = new ConnectionInfoDTO(settings.Instance, settings.User, settings.Password);

            var output = toLines(databasesSelected, connectionInfo, false);

            if (settings.Display)
                AnsiConsole.MarkupLine(string.Join(Environment.NewLine,output));
            
            output = toLines(databasesSelected, connectionInfo, true);

            if (!string.IsNullOrEmpty(settings.ExportPath) || !string.IsNullOrWhiteSpace(settings.ExportPath))
                CreateExportFile(settings.ExportPath, output);
            
            if (!string.IsNullOrEmpty(settings.SearchDirectory) || !string.IsNullOrWhiteSpace(settings.SearchDirectory))
            {
                WriteInAppSettingsFile(settings, output);
                new Panel(new Text("Successful operations")).Formatted().Write();
            }

            return 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static IEnumerable<DatabaseModel> selectDatabases(ConnectionStringsSettings settings)
    {
        var databases = ConnectionStringsSingleton.Instance.Lines();

        var databasesNames = databases.Select(x => x.Name);

        if (settings.SelectDatabases)
            databasesNames = MultiSelectDisplay.Execute(databases.Select(x => x.Name), "databases");

        var databasesSelected = DatabasesTRA.GetConnectionLinesByNames(databasesNames, databases);

        return databasesSelected;
    }

    private static void CreateExportFile(string exportPath, IEnumerable<string> textLines)
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

                CreateFileTRA.WriteInFile(exportPath, textLines);

                var panel = new Panel(
                string.Format(FileMsg.INF004, $"[blue]{exportPath}[/]"))
                    .Formatted();

                Thread.Sleep(1000);
                AnsiConsole.Write(panel);
            });
    }

    private static void WriteInAppSettingsFile(ConnectionStringsSettings settings, IEnumerable<string> strings)
    {
        var dir = new DirectoryInfo(settings.SearchDirectory);

        if (!dir.Exists)
            throw new DirectoryNotFoundException(settings.SearchDirectory);

        var files = dir.GetFiles((settings.Json ? "appSettings.json" : "*.config"), SearchOption.AllDirectories);

        var filesPath = files
            .Where(x => !x.FullName.Contains("bin"))
            .Select(x => x.FullName);

        var filesSelected = MultiSelectDisplay.Execute(filesPath, "databases");


        Action<string, IEnumerable<string>, bool> func = settings.Overwrite ? OverwriteConnectionStrings : WriteMoreConnectionStrings;

        foreach (var filePath in filesSelected)
        {
            func(filePath, strings, settings.Json);
        }
    }

    private static void OverwriteConnectionStrings(string filePath, IEnumerable<string> appSettings, bool isJson)
    {
        var lines = File.ReadAllText(filePath);

        string regexPattern = isJson
            ? @"""ConnectionStrings""\s*:\s*{[^}]*}"
            : @"<ConnectionStrings>([\s\S]*?)<\/ConnectionStrings>";

        StringBuilder sb = new StringBuilder();
        if (isJson)
            sb.AppendLine("\"ConnectionStrings\" : {");
        else
            sb.AppendLine("<connectionStrings>");

        for (int i = 0; i < appSettings.Count(); i++)
        {
            if (i == appSettings.Count() - 1)
                sb.AppendLine($"\t\t{appSettings.ElementAt(i)}");
            else
                sb.AppendLine($"\t\t{appSettings.ElementAt(i)}{isJson: ',' : ''}");
        }
        if (isJson)
            sb.Append("\t}");
        else
            sb.AppendLine("</connectionStrings>");

        string novoTexto = Regex.Replace(lines, regexPattern, sb.ToString(), RegexOptions.IgnoreCase);
        File.WriteAllText(filePath, novoTexto);
    }

    private static void WriteMoreConnectionStrings(string filePath, IEnumerable<string> appSettings, bool isJson)
    {
        var lines = File.ReadAllLines(filePath).ToList();

        var startAppSettings = -1;
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].Contains("ConnectionStrings", StringComparison.InvariantCultureIgnoreCase))
            {
                startAppSettings = i;
                break;
            }

        }

        List<string> newAppSettings = new List<string>();
        for (int i = 0; i < appSettings.Count(); i++)
        {
            if (i == appSettings.Count() - 1)
                newAppSettings.Add($"\t\t{appSettings.ElementAt(i)}");
            else
                newAppSettings.Add($"\t\t{appSettings.ElementAt(i)}{(isJson ? ',' : "")}");
        }

        var newLines = lines.Take(startAppSettings + 1).ToList();
        newLines.AddRange(newAppSettings);

        var linesAfter = lines.Skip(startAppSettings + 1).ToList();
        newLines.AddRange(linesAfter);


        File.WriteAllLines(filePath, newLines);
    }
}
