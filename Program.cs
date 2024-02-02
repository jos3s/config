using config.Branchs;
using config.Exceptions;
using config.Utils.Extensions;
using config.Utils.Messages.Documentation;

using Spectre.Console;
using Spectre.Console.Cli;

try
{
    var app = new CommandApp();

    app.Configure(config =>
    {
        config
            .UseConnectionStringsBranch()
            .UseAppSettingsBranch()
            .UseDataBranch();

        config.SetExceptionHandler((ex) =>
        {
            if(ex is FileException)
            {
                var texts = new List<Text>
                {
                    new ($"{ex.Message}",new Style(Color.Red)),
                    new ($"Execute 'config {DocumentationMsg.BRANCH001} {DocumentationMsg.COMMAND007}' to create configuration files.")
                };

                new Panel(new Rows(texts))
                    .Formatted()
                    .Write();
            
            }
            else
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        });
    });

    app.Run(args);
}
catch (Exception ex)
{
	AnsiConsole.WriteException(ex);
}