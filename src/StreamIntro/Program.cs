using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace StreamIntro
{
    class Program
    {
        private static readonly string[] TaskNames =
        {
            "Making coffee",
            "Updating tools",
            "Browsing Twitter",
            "Finding glasses",
            "Plugging in lights",
            "Learning C#",
            "Fiddling with layout",
        };

        static async Task<int> Main(string[] args)
        {
            var startTime = args.Length != 1 ? "19:00" : args[0];
            if (!TimeSpan.TryParse(startTime, out var time))
            {
                Console.Error.WriteLine("Invalid time");
                return 1;
            }

            var timeToGo = time - DateTimeOffset.Now.TimeOfDay;
            var secondsToGo = timeToGo.TotalSeconds;

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[lime]Mark Rendle will be live at {time.Hours:00}:{time.Minutes:00}...[/]");
            AnsiConsole.WriteLine();

            await AnsiConsole.Progress()
                .Columns(new ProgressColumn[]
                {
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                    new RemainingTimeColumn(),
                    new SpinnerColumn()
                })
                .StartAsync(async ctx =>
                {
                    // Add items in random order. Only first 7 will be added.
                    var items = new ProgressItems(ctx, secondsToGo, TaskNames.OrderBy(_ => Guid.NewGuid()));

                    while (!ctx.IsFinished)
                    {
                        await Task.Delay(1000);
                        items.Increment();
                    }
                });
            
            AnsiConsole.MarkupLine("Intro made with Spectre.Console. Check it out: [deepskyblue1]https://spectresystems.github.io/spectre.console/[/]");

            return 0;
        }
    }
}