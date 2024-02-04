using config.Features._Shared;
using config.Transaction;
using config.Utils.Extensions;
using config.Utils.Messages;

using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Features.Data;
internal class CreateDataFilesCommand : Command<BaseSettings>
{
    private string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory + "AppData";

    private string _settingsFile = @"\Settings.json";

    private string _databasesFile = @"\Databases.json";

    public override int Execute(CommandContext context, BaseSettings settings)
    {
        var directoryInfo = CreateFileTRA
            .CreateDirectoryIfNotExistes(_baseDirectory);

        var settingsExists = CreateFileTRA.FileExists(_baseDirectory + _settingsFile);
        var databasesExists = CreateFileTRA.FileExists(_baseDirectory + _databasesFile);

        var filesToCreate = new List<string>();

        if (!settingsExists)
            filesToCreate.Add(directoryInfo.FullName + @"\Settings.json");

        if (!databasesExists)
            filesToCreate.Add(directoryInfo.FullName + @"\Databases.json");

        Array.ForEach(filesToCreate.ToArray(), CreateFile);

        if (!settingsExists || !databasesExists)
            new Panel(new Text(FileMsg.INF006, new Style(Color.Green))).Formatted().Write();
        else
            new Panel(new Text("Operation not completed: Configuration files already exist.", new Style(Color.Yellow))).Formatted().Write();

        return 0;
    }

    private static void CreateFile(string path)
    {
        File.AppendAllText(path, "[]");
    }
}


