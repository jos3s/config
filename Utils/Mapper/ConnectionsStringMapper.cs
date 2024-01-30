using System.Text;
using config.Utils.Messages;
using config.Features.Database.Shared;

namespace config.Utils.Mapper
{
    internal class ConnectionsStringMapper
    {
        public static string ToConfig(IEnumerable<DatabaseModel> connectionLines, string user, string password, string instance, bool toFile = false)
        {
            StringBuilder stringBuilder = new();

            var markupStyle = "[darkseagreen4_1]{0}[/]";

            var instanceString = !toFile ? string.Format(markupStyle, instance) : instance;
            var userIdString = !toFile ? string.Format(markupStyle, user) : user;
            var passwordString = !toFile ? string.Format(markupStyle, password) : password;


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

        public static string ToJson(IEnumerable<DatabaseModel> connectionLines, string user, string password, string instance, bool toFile = false)
        {
            var markupStyle = "[darkseagreen4_1]{0}[/]";

            var instanceString = !toFile ? string.Format(markupStyle, instance) : instance;
            var userIdString = !toFile ? string.Format(markupStyle, user) : user;
            var passwordString = !toFile ? string.Format(markupStyle, password) : password;

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
