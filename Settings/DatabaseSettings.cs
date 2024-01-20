using Spectre.Console.Cli;
using System.ComponentModel;

namespace config.Settings
{
	internal class DatabaseSettings : CommandSettings
	{
		[CommandArgument(0, "[NAME]")]
		public string Name { get; set; }

		[CommandArgument(0, "[CATALOG]")]
		public string Catalog { get; set; }

		[CommandArgument(0, "[APPNAME]")]
		public string AplicationName { get; set; }

		[CommandOption("-t|--timeout")]
		[DefaultValue(180)]
		public int connectionTimeoutt { get; set; }

		public string ProviderName { get; set; } = "System.Data.SqlClient";
		public string Pooling { get; set; } = "True";
		public string Encrypt { get; set; } = "True";


	}
}
