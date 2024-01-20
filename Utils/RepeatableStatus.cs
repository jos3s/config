using config.Models.DTOs;
using Spectre.Console;

namespace config.Utils;
internal class RepeatableStatus
{
    public static int Run(List<string> strings, string initialMsg,string repeatableMsg, string finalMsg, int sleep = 1000)
    {
        AnsiConsole.Status()
            .Start(initialMsg, ctx =>
            {
                Thread.Sleep(sleep);

                foreach (var s in strings)
                {
                    ctx.Status(repeatableMsg);
                    ctx.Spinner(Spinner.Known.Dots);
                    ctx.SpinnerStyle(Style.Parse("green"));
                    Thread.Sleep(sleep);
                    AnsiConsole.MarkupLine(s);
                }

                AnsiConsole.MarkupLine($"[green]{finalMsg}[/]");
            });

        return 0;
    }

    public static void Run(RepeatableStatusMsg msgs, int count, int sleep = 1000)
    {
        AnsiConsole.Status()
            .Start(msgs.InitalMsg, ctx =>
            {
                Thread.Sleep(sleep);

                for (int i = 0; i < count; i++)
                {
                    ctx.Status(msgs.RepeatableMsg);
                    ctx.Spinner(Spinner.Known.Dots);
                    ctx.SpinnerStyle(Style.Parse("green"));
                    Thread.Sleep(sleep);
                }

                AnsiConsole.MarkupLine($"[green]{msgs.FinalMsg}[/]");
            });
    }
}
