using config.Settings;

using Spectre.Console.Cli;
using System.ComponentModel;

namespace config.Features.Database.Settings
{
    internal class CreateDatabaseSettings : BaseSettings
    {
        [CommandArgument(0, "<name>")]
        public string Name { get; set; }

        [CommandArgument(1, "<catalog>")]
        public string Catalog { get; set; }

        [CommandArgument(2, "<appname>")]
        public string AplicationName { get; set; }

        [CommandOption("-t|--timeout")]
        [DefaultValue(180)]
        public int connectionTimeoutt { get; set; }

        public string ProviderName { get; set; } = "System.Data.SqlClient";
        public string Pooling { get; set; } = "True";
        public string Encrypt { get; set; } = "True";

    }
}
