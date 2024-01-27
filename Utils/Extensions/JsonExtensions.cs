using Spectre.Console;
using Spectre.Console.Json;

namespace config.Utils.Extensions;
internal static class JsonExtensions
{
    public static JsonText StyledJsonText(this JsonText text)
    {
        text.BracesColor(Color.Grey84)
            .BracketColor(Color.Grey84)
            .ColonColor(Color.Yellow)
            .CommaColor(Color.Grey84)
            .StringColor(Color.Green)
            .NumberColor(Color.Blue)
            .BooleanColor(Color.Red)
            .NullColor(Color.Green).MemberColor(Color.White);
        return text;
    }
}
