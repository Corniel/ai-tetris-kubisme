namespace Tetris.Benchmarks;

public class MoveGeneration
{
    [Params(Shape.I, Shape.J, Shape.L, Shape.O, Shape.S, Shape.T, Shape.Z)]
    public Shape Shape { get; set; }

    [GlobalSetup]
    public void Init()
    {
        Console.WriteLine($"{nameof(Start)}: {Start().Count}");
        Console.WriteLine($"{nameof(Mixed)}: {Mixed().Count}");
    }

    [Benchmark(Baseline = true)]
    public IReadOnlyCollection<MoveCandidate> Start()
    => new MoveGenerator(Field.Start, Data.Blocks.Spawn(Shape)).ToArray();

    [Benchmark]
    public IReadOnlyCollection<MoveCandidate> Mixed()
    {
        var moves = new List<MoveCandidate>();
        foreach (var field in Data.Fields)
        {
            moves.AddRange(new MoveGenerator(field, Data.Blocks.Spawn(Shape)));
        }
        return moves;
    }
}
