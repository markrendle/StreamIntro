using Spectre.Console;

namespace StreamIntro
{
    internal readonly struct ProgressItem
    {
        private readonly ProgressTask _task;
        private readonly double _increment;

        public ProgressItem(ProgressContext ctx, string name, string color, double increment)
        {
            _increment = increment;
            _task = ctx.AddTask($"[{color}]{name}[/]");
        }

        public void Increment()
        {
            _task.Increment(_increment);
        }
    }
}