using NUnit.Framework;
using System;
using System.Diagnostics;

namespace Testris.Specs
{
    internal static class Speed
    {
        public static void Runs(Action action, int runs = 1, TimeSpan? maxDuration = default)
        {
            var sw = Stopwatch.StartNew();
            for (var run = 0; run < runs; run++)
            {
                action();
            }
            sw.Stop();

            var elapsed = sw.Elapsed / runs;

            var message = $"Expected duration below: {maxDuration.Format()}" + Environment.NewLine +
                $"Actual duration: {elapsed.Format()}";

            Console.WriteLine(message);

            Assert.IsTrue(!maxDuration.HasValue || maxDuration > elapsed, message);
        }

        private static string Format(this TimeSpan time) => Format((TimeSpan?)time);
        private static string Format(this TimeSpan? time)
        => time.HasValue
            ? $"{time.Value.TotalMilliseconds:#,##0.000} ms"
            : "?";
    }
}
