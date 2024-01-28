using System.Text;
using config.Utils.Messages;
using config.Features.Database.Models;

namespace config.Utils.Mapper
{
    internal class ConnectionsStringMapper
    {
        public static string ToConfig(IEnumerable<Database> connectionLines, string user, string password, string instance)
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

        public static string ToConfigLine(Database connectionLine, string instanceString, string userIdString, string passwordString)
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

        public static string ToJson(IEnumerable<Database> connectionLines, string user, string password, string instance)
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

        public static string ToJsonLine(Database connectionLine, string instanceString, string userIdString, string passwordString)
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
