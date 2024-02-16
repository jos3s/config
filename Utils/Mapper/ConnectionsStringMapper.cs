using config.DTOs;
using config.Features.Database.Shared;
using config.Utils.Messages;

using System.Text;

namespace config.Utils.Mapper;

internal class ConnectionsStringMapper
{
    public static List<string> ToConfigList(IEnumerable<DatabaseModel> connectionLines, ConnectionInfoDTO connectionInfo, bool toFile = false)
    {
        StringBuilder stringBuilder = new();

        var markupStyle = "[darkseagreen4_1]{0}[/]";

        var instanceString = !toFile ? string.Format(markupStyle, connectionInfo.Instance) : connectionInfo.Instance;
        var userIdString = !toFile ? string.Format(markupStyle, connectionInfo.User) : connectionInfo.User;
        var passwordString = !toFile ? string.Format(markupStyle, connectionInfo.Password) : connectionInfo.Password;


        var toConfigList = new List<string>();
        for (int i = 0; i < connectionLines.Count(); i++)
        {
            toConfigList.Add(
                ToConfigLine(connectionLines.ElementAt(i), instanceString, userIdString, passwordString)
                + $"{(i == connectionLines.Count() - 1 ? ',' : "")}"
            );
        }

        return toConfigList;
    }

    public static string ToConfig(IEnumerable<DatabaseModel> connectionLines, ConnectionInfoDTO connectionInfo, bool toFile = false)
    {
        StringBuilder stringBuilder = new();

        var markupStyle = "[darkseagreen4_1]{0}[/]";

        var instanceString = !toFile ? string.Format(markupStyle, connectionInfo.Instance) : connectionInfo.Instance;
        var userIdString = !toFile ? string.Format(markupStyle, connectionInfo.User) : connectionInfo.User;
        var passwordString = !toFile ? string.Format(markupStyle, connectionInfo.Password) : connectionInfo.Password;


        foreach (var connectionLine in connectionLines)
        {
            stringBuilder.AppendLine(ToConfigLine(connectionLine, instanceString, userIdString, passwordString));
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

    public static List<string> ToJsonList(IEnumerable<DatabaseModel> connectionLines, ConnectionInfoDTO connectionInfo, bool toFile = false)
    {
        StringBuilder stringBuilder = new();

        var markupStyle = "[darkseagreen4_1]{0}[/]";

        var instanceString = !toFile ? string.Format(markupStyle, connectionInfo.Instance) : connectionInfo.Instance;
        var userIdString = !toFile ? string.Format(markupStyle, connectionInfo.User) : connectionInfo.User;
        var passwordString = !toFile ? string.Format(markupStyle, connectionInfo.Password) : connectionInfo.Password;


        var toJsonList = new List<string>();
        for (int i = 0; i < connectionLines.Count(); i++)
        {
            toJsonList.Add(
                ToJsonLine(connectionLines.ElementAt(i), instanceString, userIdString, passwordString)
                + $"{(i == connectionLines.Count() - 1 ? ',' : "")}"
            );
        }

        return toJsonList;
    }

    public static string ToJson(IEnumerable<DatabaseModel> connectionLines, ConnectionInfoDTO connectionInfo, bool toFile = false)
    {
        var markupStyle = "[darkseagreen4_1]{0}[/]";

        var instanceString = !toFile ? string.Format(markupStyle, connectionInfo.Instance) : connectionInfo.Instance;
        var userIdString = !toFile ? string.Format(markupStyle, connectionInfo.User) : connectionInfo.User;
        var passwordString = !toFile ? string.Format(markupStyle, connectionInfo.Password) : connectionInfo.Password;

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
