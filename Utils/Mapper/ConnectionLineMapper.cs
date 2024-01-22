using config.Models;
using System.Text;
using config.Utils.Messages;

namespace config.Utils.Mapper
{
    internal class ConnectionLineMapper
    {
        public static string ToConfig(IEnumerable<ConnectionLine> connectionLines, string user, string password, string instance)
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

        public static string ToConfigLine(ConnectionLine connectionLine, string instanceString, string userIdString, string passwordString)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine(string.Format(StringsFormatedMsg.CONNCONFIG,
                connectionLine.Name,
                connectionLine.ProviderName,
                instanceString,
                connectionLine.ConnectionString.InitalCatalog,
                userIdString,
                passwordString,
                connectionLine.ConnectionString.Pooling,
                connectionLine.ConnectionString.ConnectTimeout,
                connectionLine.ConnectionString.AplicationName));
            return stringBuilder.ToString();
        }

        public static string ToJson(IEnumerable<ConnectionLine> connectionLines, string user, string password, string instance)
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

        public static string ToJsonLine(ConnectionLine connectionLine, string instanceString, string userIdString, string passwordString)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.Append(string.Format(StringsFormatedMsg.CONNJSON,
                connectionLine.Name,
                instanceString,
                connectionLine.ConnectionString.InitalCatalog,
                userIdString,
                passwordString,
                connectionLine.ConnectionString.Pooling,
                connectionLine.ConnectionString.ConnectTimeout,
                connectionLine.ConnectionString.AplicationName));

            return stringBuilder.ToString();
        }

    }
}
