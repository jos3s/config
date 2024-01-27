using config.Settings.AppSettings;
using config.Singleton;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Json;
using System.Text.Json;
using config.Utils.Extensions;

namespace config.Commands.AppSettings;
internal class DisplayKeysCommand : Command<DisplayKeysSettings>
{
    public override int Execute(CommandContext context, DisplayKeysSettings settings)
    {
        var keysGroups = AppSettingsSingleton.Instance.Lines();

        if (settings.DisplayInJson)
        {
            var keysInJson = JsonSerializer.Serialize(keysGroups);

            AnsiConsole.Write(new JsonText(keysInJson).StyledJsonText());
            return 0;
        }

        var table = new Table();

        table.AddColumns("Group", "Key", "Value");

        foreach (var keyGroup in keysGroups)
        {
            var groupName = keyGroup.GroupName;

            var keys = keyGroup.Keys;

            for (int i = 0; i < keys.Count(); i++)
            {
                table.AddRow(i == 0 ? $"[green]{groupName}[/]" : "", keys[i].Key, keys[i].Value);
            }
        }

        AnsiConsole.Write(table);

        return 0;
    }
}
