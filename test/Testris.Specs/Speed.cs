using NUnit.Framework;
using System;
using System.Diagnostics;

namespace Testris.Specs
{
    internal static class Speed
    {
        public static T Measure<T>(Func<T> func)
        {
            var sw = Stopwatch.StartNew();
            var result = func();
            sw.Stop();
            Console.WriteLine(sw.Elapsed.Format());
            return result;
        }
        public static TimeSpan Runs(Action action, TimeSpan? maxDuration = default)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();

            var elapsed = sw.Elapsed;

            var message = $"Expected duration below: {maxDuration.Format()}" + Environment.NewLine +
                $"Actual duration: {elapsed.Format()}";

            Console.WriteLine(message);

            Assert.IsTrue(!maxDuration.HasValue || maxDuration > elapsed, message);

            return elapsed;
        }

        private static string Format(this TimeSpan time) => Format((TimeSpan?)time);
        private static string Format(this TimeSpan? time)
        => time.HasValue
            ? $"{time.Value.TotalMilliseconds:#,##0.000} ms"
            : "?";
    }
}
