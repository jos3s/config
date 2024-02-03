using config.Branchs;
using config.Exceptions;

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

        config.SetExceptionHandler(ExceptionHandlers.Handler);
    });

    app.Run(args);
}
catch (Exception ex)
{
	AnsiConsole.WriteException(ex);
}