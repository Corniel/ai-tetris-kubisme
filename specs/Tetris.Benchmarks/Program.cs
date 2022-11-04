using BenchmarkDotNet.Running;

namespace Tetris.Benchmarks;

static class Program
{
    static void Main(params string[] args)
    {
        if (args.Any())
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
        else
        {
            BenchmarkRunner.Run<MoveGeneration>();
        }
    }

}
