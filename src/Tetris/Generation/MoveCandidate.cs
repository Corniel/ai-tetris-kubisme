namespace Tetris.Generation;

public readonly struct MoveCandidate
{
    public readonly Block Block;
    public readonly Steps Steps;

    public MoveCandidate(Block block)
        : this(block, Steps.None) { }

    public MoveCandidate(Block block, Step step)
        : this(block, Steps.None.Add(step)) { }

    private MoveCandidate(Block block, Steps steps)
    {
        Block = block;
        Steps = steps;
    }

    public MoveCandidate Down()
        => new(Block.Down, Steps.AddDown());

    public MoveCandidate Next(MoveCandidate candidate)
        => new(candidate.Block, Steps.Add(candidate.Steps.First));

    internal int Offset => Block.Offset;
    internal short Id => Block.Id; 
    internal short Primary => Block.Primary;

    internal IEnumerable<MoveCandidate> Nexts => Block.Nexts;
    internal IEnumerable<MoveCandidate> Others => Block.Others;
}
