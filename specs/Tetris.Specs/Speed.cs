namespace Specs;

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
        
        if (maxDuration is { } span)
        {
            elapsed.Should().BeLessThan(span, because: message);
        }

    }

    private static string Format(this TimeSpan time) => Format((TimeSpan?)time);
    private static string Format(this TimeSpan? time)
    => time.HasValue
        ? $"{time.Value.TotalMilliseconds:#,##0.000} ms"
        : "?";
}
