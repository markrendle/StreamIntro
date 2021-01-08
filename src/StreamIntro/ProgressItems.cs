using System.Collections.Generic;
using Spectre.Console;

namespace StreamIntro
{
    internal class ProgressItems
    {
        private static readonly string[] Colors = {"red", "darkorange", "gold1", "lime", "deepskyblue1", "blueviolet", "darkviolet_1"};
        private static readonly double[] Multipliers = {150, 200, 250, 175, 300, 125, 100};
        private readonly List<ProgressItem> _items;
        private readonly ProgressContext _ctx;
        private readonly double _secondsToGo;

        public ProgressItems(ProgressContext ctx, double secondsToGo, IEnumerable<string> names)
        {
            _ctx = ctx;
            _secondsToGo = secondsToGo;
            _items = new List<ProgressItem>();
            foreach (var name in names)
            {
                Add(name);
            }
        }

        private void Add(string name)
        {
            int i = _items.Count;
            if (i > 6) return;
            _items.Add(new ProgressItem(_ctx, name, Colors[i], Multipliers[i] / _secondsToGo));
        }

        public void Increment()
        {
            _items.ForEach(i => i.Increment());
        }
    }
}