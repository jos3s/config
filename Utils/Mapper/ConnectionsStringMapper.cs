using System.Text;
using config.Utils.Messages;
using config.Features.Database.Shared;

namespace config.Utils.Mapper
{
    internal class ConnectionsStringMapper
    {
        public static string ToConfig(IEnumerable<DatabaseModel> connectionLines, string user, string password, string instance)
        {
            StringBuilder stringBuilder = new();

            var instanceString = $"[darkseagreen4_1]{instance}[/]";
            var userIdString = $"[darkseagreen4_1]{user}[/]";
            var passwordString = $"[darkseagreen4_1]{password}[/]";

            foreach (var connectionLine in connectionLines)
            {
                stringBuilder.Append(ToConfigLine(connectionLine, instanceString, userIdString, passwordString));
                stringBuilder.Append(",");
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        public static string ToConfigLine(DatabaseModel connectionLine, string instanceString, string userIdString, string passwordString)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.Append(string.Format(StringsFormatedMsg.CONNCONFIG,
                connectionLine.Name,
                connectionLine.ProviderName,
                instanceString,
                connectionLine.InitalCatalog,
                userIdString,
                passwordString,
                connectionLine.Pooling,
                connectionLine.ConnectTimeout,
                connectionLine.AplicationName));
            return stringBuilder.ToString();
        }

        public static string ToJson(IEnumerable<DatabaseModel> connectionLines, string user, string password, string instance)
        {

            var instanceString = $"[darkseagreen4_1]{instance}[/]";
            var userIdString = $"[darkseagreen4_1]{user}[/]";
            var passwordString = $"[darkseagreen4_1]{password}[/]";

            StringBuilder stringBuilder = new();

            foreach (var connectionLine in connectionLines)
            {
                stringBuilder.Append(ToJsonLine(connectionLine, instanceString, userIdString, passwordString));
                stringBuilder.Append(",");
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        public static string ToJsonLine(DatabaseModel connectionLine, string instanceString, string userIdString, string passwordString)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.Append(string.Format(StringsFormatedMsg.CONNJSON,
                connectionLine.Name,
                instanceString,
                connectionLine.InitalCatalog,
                userIdString,
                passwordString,
                connectionLine.Pooling,
                connectionLine.ConnectTimeout,
                connectionLine.AplicationName));

            return stringBuilder.ToString();
        }

    }
}
