namespace Tetris.Benchmarks;

public class MoveProcessing
{
    private readonly Dictionary<Field, IReadOnlyCollection<MoveCandidate>> Candidates = new();

    [GlobalSetup]
    public void Init()
    {
        Candidates.Clear();

        foreach (var field in Data.Fields)
        {
            var list = new List<MoveCandidate>();

            foreach (var shape in Shapes.All)
            {
                list.AddRange(new MoveGenerator(field, Data.Blocks.Spawn(shape)));
            }
            Candidates[field] = list;
        }
        Console.WriteLine($"total: {Candidates.Values.Sum(c => c.Count)}");
    }

    [Benchmark(Baseline = true)]
    public IReadOnlyCollection<Move> Without_steps()
    {
        var moves = new List<Move>();
        foreach(var kvp in Candidates)
        {
            foreach (var candidate in kvp.Value)
            {
                moves.Add(kvp.Key.Move(candidate.Block, candidate.Steps.Last.IsRotation()));
            }
        }
        return moves;
    }

    [Benchmark]
    public IReadOnlyCollection<Move> With_steps()
    {
        var moves = new List<Move>();
        foreach (var kvp in Candidates)
        {
            foreach (var candidate in kvp.Value)
            {
                moves.Add(kvp.Key.Move(candidate.Block, candidate.Steps));
            }
        }
        return moves;
    }
}
