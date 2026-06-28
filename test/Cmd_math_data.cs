using System;
using System.Collections.Generic;
using cmdaxe;

namespace test
{
    [Command(name: "data", group: "math", desc: "Examines input number data")]
    class Cmd_math_data : Command
    {
        #region required

        [Required(desc: "Numbers")]
        double[] numbers;

        #endregion

        #region methods

        public override void Main()
        {
            // Sort numbers
            double[] sorted = (numbers is null) ? [] : [..numbers];
            Array.Sort(sorted);
            // Print numbers
            Console.Write("[ ");
            foreach (var number in sorted)
            { Console.Write(number); Console.Write(", "); }
            Console.WriteLine("]");
            // Exit if array is empty
            if (sorted.Length == 0) return;
            // Compute total and mean
            var total = 0.0;
            foreach (var number in sorted)
                total += number;
            var mean = total / sorted.Length;
            // Compute median
            var medIndex = sorted.Length / 2;
            var median = sorted[medIndex];
            if ((sorted.Length % 2) == 0) median = (median + sorted[medIndex - 1]) / 2;
            // Compute mode
            var occurs = new Dictionary<double, int>();
            foreach (var number in sorted)
            { if (!occurs.TryAdd(number, 1)) occurs[number] += 1; }
            var modeCount = 0;
            foreach (var occur in occurs)
            { if (modeCount < occur.Value) modeCount = occur.Value; }
            var mode = new List<double>();
            foreach (var occur in occurs)
            { if (occur.Value == modeCount) mode.Add(occur.Key); }
            // Print results
            const int WIDTH = 10;
            Console.Write("Total:".PadRight(WIDTH));
            Console.WriteLine(total);
            Console.Write("Mean:".PadRight(WIDTH));
            Console.WriteLine(mean);
            Console.Write("Median:".PadRight(WIDTH));
            Console.WriteLine(median);
            Console.Write("Mode:".PadRight(WIDTH));
            for (int i = 0; (i + 1) < mode.Count; ++i)
            { Console.Write(mode[i]); Console.Write(", "); }
            Console.WriteLine(mode[^1]);
        }

        #endregion

    }
}