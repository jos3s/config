using config.Features._Shared;
using config.Transaction;
using config.Utils;
using config.Utils.Extensions;
using config.Utils.Messages;

using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Features.Data;
internal class CreateDataFilesCommand : Command<BaseSettings>
{
    private string _baseDirectory = ConfigPathHelper.FolderPath;

    public override int Execute(CommandContext context, BaseSettings settings)
    {
        var directoryInfo = CreateFileTRA
            .CreateDirectoryIfNotExistes(_baseDirectory);

        var settingsExists = CreateFileTRA.FileExists(ConfigPathHelper.SettingsPath);
        var databasesExists = CreateFileTRA.FileExists(ConfigPathHelper.DatabasesPath);

        var filesToCreate = new List<string>();

        if (!settingsExists)
            filesToCreate.Add(directoryInfo.FullName + @"\" + ConfigPathHelper.SettingsFileName);

        if (!databasesExists)
            filesToCreate.Add(directoryInfo.FullName + @"\" + ConfigPathHelper.DatabasesFileName);

        filesToCreate.ForEach(CreateFile);

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


