using config.Features._Shared;
using config.Transaction;
using config.Utils.Extensions;
using config.Utils.Messages;

using Spectre.Console;
using Spectre.Console.Cli;

namespace config.Features.Data;
internal class CreateDataFilesCommand : Command<BaseSettings>
{
    private string _baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "AppData";

    private string _settingsFile = @"\Settings.json";

    private string _databasesFile = @"\Databases.json";

    public override int Execute(CommandContext context, BaseSettings settings)
    {
        var directoryInfo = CreateFileTRA
            .CreateDirectoryIfNotExistes(_baseDirectory);

        var settingsExists = CreateFileTRA.FileExists(_baseDirectory + _settingsFile);
        var databasesExists = CreateFileTRA.FileExists(_baseDirectory + _databasesFile);

        if (!settingsExists)
            File.Create(directoryInfo.FullName + @"\Settings.json");

        if (!databasesExists)
            File.Create(directoryInfo.FullName + @"\Databases.json");

        if (!settingsExists || !databasesExists)
            new Panel(new Text(FileMsg.INF006, new Style(Color.Green))).Formatted().Write();
        else
            new Panel(new Text("Operation not completed: Configuration files already exist.", new Style(Color.Yellow))).Formatted().Write();

        return 0;
    }
}


