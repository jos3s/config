﻿using config.Models;
using System.Text;

namespace config.Utils
{
    internal class CreateLine
	{
		public static string Config(IEnumerable<ConnectionLine> connectionLines, string user, string password, string instance)
		{
			StringBuilder stringBuilder = new();

			var instanceString = $"[darkseagreen4_1]Data Source={instance}[/];";
			var userIdString = $"[darkseagreen4_1]User ID={user}[/];";
			var passwordString = $"[darkseagreen4_1]User ID={password}[/];";

			foreach (var connectionLine in connectionLines)
            {
                stringBuilder.AppendLine(ConfigLine(connectionLine, instanceString, userIdString, passwordString));
            }

			return stringBuilder.ToString();
		}

        public static string ConfigLine(ConnectionLine connectionLine, string instanceString, string userIdString, string passwordString)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append("<add ");

            stringBuilder.Append($"[skyblue2]Name=\"{connectionLine.Name}\"[/] ");
            stringBuilder.Append($"providerName=\"{connectionLine.ProviderName}\" ");

            stringBuilder.Append("connectionString=\"");
            stringBuilder.Append(instanceString);
            stringBuilder.Append($"Initial Catalog={connectionLine.ConnectionString.InitalCatalog};");
            stringBuilder.Append(userIdString);
            stringBuilder.Append(passwordString);
            stringBuilder.Append($"Pooling={connectionLine.ConnectionString.Pooling};");
            stringBuilder.Append($"Connect Timeout={connectionLine.ConnectionString.ConnectTimeout};");
            stringBuilder.Append($"Application Name={connectionLine.ConnectionString.AplicationName}");
            stringBuilder.Append('"');

            stringBuilder.Append("/>");
			return stringBuilder.ToString();
        }

        public static string Json(IEnumerable<ConnectionLine> connectionLines, string user, string password, string instance)
		{

			var instanceString = $"Data Source={instance};";
			var userIdString = $"User ID={user};";
			var passwordString = $"Password={password};";

			StringBuilder stringBuilder = new();

			foreach (var connectionLine in connectionLines)
			{
                stringBuilder.AppendLine(JsonLine(connectionLine, instanceString, userIdString, passwordString));
            }

			return stringBuilder.ToString();
		}

        public static string JsonLine(ConnectionLine connectionLine, string instanceString, string userIdString, string passwordString)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"\"{connectionLine.Name}\":");

            stringBuilder.Append("\"");
            stringBuilder.Append(instanceString);
            stringBuilder.Append($"Initial Catalog={connectionLine.ConnectionString.InitalCatalog};");
            stringBuilder.Append(userIdString);
            stringBuilder.Append(passwordString);
            stringBuilder.Append($"Pooling={connectionLine.ConnectionString.Pooling};");
            stringBuilder.Append($"Connect Timeout={connectionLine.ConnectionString.ConnectTimeout};");
            stringBuilder.Append($"Application Name={connectionLine.ConnectionString.AplicationName}");
            stringBuilder.Append('"');

            stringBuilder.Append(",");
            return stringBuilder.ToString();
        }

    }
}
