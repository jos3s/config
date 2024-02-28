using config.Features.Settings.Shared;
using config.Singleton;
using config.Transaction;
using config.Utils.Display;
using config.Utils.Extensions;
using config.Utils.Messages;

using Spectre.Console;
using Spectre.Console.Cli;

using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace config.Features.AppSettings;
internal class GenerateAppSettingsCommand : Command<GenerateKeysSettings>
{
    private static List<SettingsGroupModel> appSettings = SettingsSingleton.Instance.Lines();
    
    public override int Execute(CommandContext context, GenerateKeysSettings settings)
    {
        var keys = appSettings.SelectMany(x => x.Keys.Select(x => x.Key));

        if (settings.SelectKeys)
        {
            keys = MultiSelectDisplay.ExecuteForSettingsGroup(appSettings, "keys");
        }

        var strings = CreateListResult(appSettings, keys, settings.Json);

        if (!string.IsNullOrEmpty(settings.ExportPath) || !string.IsNullOrWhiteSpace(settings.ExportPath))
            CreateExportFile(settings, strings);

        if (settings.Display)
        {
            var rows = strings.Select(x => new Text(x));
            AnsiConsole.Write(new Rows(rows));
        }

        if (!string.IsNullOrEmpty(settings.SearchDirectory) || !string.IsNullOrWhiteSpace(settings.SearchDirectory))
        {
            WriteInAppSettingsFile(settings, keys);
            new Panel(new Text("Successful operations")).Formatted().Write();
        }


        return 0;
    }

    private static void CreateExportFile(GenerateKeysSettings settings, IEnumerable<string> strings)
    {
        CreateFileTRA.ValidatePath(settings.ExportPath);

        settings.ExportPath += @"\appsettings.txt";

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

                CreateFileTRA.WriteLinesInFile(settings.ExportPath, strings);

                var panel = new Panel(
                string.Format(FileMsg.INF004, $"[blue]{settings.ExportPath}[/]"))
                    .Formatted();

                Thread.Sleep(1000);
                AnsiConsole.Write(panel);
            });
    }

    private static IEnumerable<string> CreateListResult(List<SettingsGroupModel> appSettings, IEnumerable<string> keys, bool json)
    {
        var output = new List<string>();

        foreach (var appSettingsGroup in appSettings)
        {
            foreach (var key in appSettingsGroup.Keys)
            {
                if (keys.Contains(key.Key)) output.Add(!json ? key.ToConfig() : key.ToJson());
            }
        }

        return output;
    }

    private static void WriteInAppSettingsFile(GenerateKeysSettings settings, IEnumerable<string> keys)
    {
        var dir = new DirectoryInfo(settings.SearchDirectory);

        if (!dir.Exists)
            throw new DirectoryNotFoundException(settings.SearchDirectory);

        var files = dir.GetFiles((settings.Json ? "appSettings.json" : "*.config"), SearchOption.AllDirectories);

        var filesPath = files
            .Where(x => !x.FullName.Contains("bin"))
            .Select(x => x.FullName);

        var filesSelected = MultiSelectDisplay.Execute(filesPath, "AppSettings");

        var newKeys = CreateListResult(appSettings, keys, settings.Json).ToList();



        Action<string, List<string>, List<string>, bool> func = settings.OverwriteAll ? OverwriteAppSettings : WriteMoreAppSettings;

        foreach (var filePath in filesSelected)
            func(filePath, keys.ToList(), newKeys, settings.Json);
    }

    private static void OverwriteAppSettings(string filePath, List<string> keys, List<string> newKeys, bool isJson)
    {
        var lines = File.ReadAllText(filePath);

        string regexPattern = isJson
            ? @"""AppSettings""\s*:\s*{[^}]*}"
            : @"<appSettings>([\s\S]*?)<\/appSettings>";

        StringBuilder sb = new StringBuilder();

        sb.AppendLine(isJson ? "\"AppSettings\" : {" : "<appSettings>");

        for (int i = 0; i < newKeys.Count; i++)
        {
            var line = newKeys.ElementAt(i);
            var finalChar = isJson ? "," : "";
            sb.AppendLine($"{Environment.NewLine}{Environment.NewLine}{line}{finalChar}");
        }

        sb.Append(isJson ? "\t}" : "</appSettings>");

        string novoTexto = Regex.Replace(lines, regexPattern,sb.ToString(), RegexOptions.IgnoreCase);
        File.WriteAllText(filePath, novoTexto);
    }

    private static void WriteMoreAppSettings(string filePath, List<string> keys, List<string> newKeys, bool isJson)
    {
        var lines = File.ReadAllLines(filePath).ToList();

        var startAppSettings = -1;
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].Contains("AppSettings", StringComparison.InvariantCultureIgnoreCase))
            {
                startAppSettings = i;
                break;
            }
        }

        var duplicatedKeys = new List<string>();
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < keys.Count; j++)
            {
                if (lines[i].Contains(keys[j]))
                {
                    duplicatedKeys.Add(keys[j]);
                    var line = newKeys.Where(x => x.Contains(keys[j])).First();
                    var finalChar = lines[i].Contains(',') ? "," : "";
                    lines[i] = ($"{Environment.NewLine}{Environment.NewLine}{line}{finalChar}");
                }
            }
        }

        keys.RemoveAll(x => duplicatedKeys.Contains(x));

        var newLines = new List<string>();

        if (keys.Count > 0)
        {
            newKeys = CreateListResult(appSettings, keys, isJson).ToList();
            List<string> newAppSettings = new List<string>();
            for (int i = 0; i < newKeys.Count; i++)
                newAppSettings.Add($"\t\t{newKeys.ElementAt(i)}{(isJson ? ',' : "")}");

            newLines = lines.Take(startAppSettings + 1).ToList();
            newLines.AddRange(newAppSettings);

            var linesAfter = lines.Skip(startAppSettings + 1).ToList();
            newLines.AddRange(linesAfter);
        }
        else
            newLines = lines;

        File.WriteAllLines(filePath, newLines);
    }
}
