using config.Branchs;
using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(config =>
{
	config.UseConnectionStringsBranch();
	config.UseDatabasesInstancesBranch();
	config.UseAppSettingsBranch();
});

try
{
	app.Run(args);
}
catch (Exception ex)
{
	AnsiConsole.WriteException(ex);
}