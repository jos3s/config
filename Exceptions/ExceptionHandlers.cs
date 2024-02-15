using config.Utils.Extensions;
using config.Utils.Messages;
using config.Utils.Messages.Documentation;

using Spectre.Console;

namespace config.Exceptions;
internal static class ExceptionHandlers
{
    public static void Handler(Exception ex)
    {
        if (ex is AppDataException)
        {
            var texts = new List<Text>
                {
                    new ($"{ex.Message}",new Style(Color.Red)),
                    new ($"Execute 'config {DocumentationMsg.BRANCH001} {DocumentationMsg.COMMAND007}' to create configuration files.")
                };

            new Panel(new Rows(texts)).Formatted().Write();
        }
        else if(ex is DirectoryNotFoundException)
        {
            new Panel(new Text(string.Format(ExceptionMsg.EXC0002, ex.Message), new Style(Color.Red))).Formatted().Write();
        }
        else
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
    }
}
