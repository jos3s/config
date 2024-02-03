using Spectre.Console;
using Spectre.Console.Rendering;

namespace config.Utils.Extensions;
internal static class SpectreConsoleExtensions
{
    public static IRenderable Write(this IRenderable renderable)
    {
        AnsiConsole.Write(renderable);
        return renderable;
    }

    public static Panel Formatted(this Panel panel)
    {
        return panel.Border(BoxBorder.Rounded)
                    .Padding(2, 2, 2, 2)
                    .Expand();
    }
}
