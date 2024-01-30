using Spectre.Console;

namespace config.Utils.Extensions;
internal static class SpectreConsoleExtensions
{
    public static Panel Formatted(this Panel panel)
    {
        return panel.Border(BoxBorder.Rounded)
                    .Padding(2, 2, 2, 2)
                    .Expand();
    }
}
