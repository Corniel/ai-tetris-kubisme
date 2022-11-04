using SmartAss;

namespace Tetris;

public partial class Blocks : IEnumerable<Block>
{
    private Blocks(Block[] spawns, Rows rows, RotationSystem rotationSystem)
    {
        this.spawns = spawns;
        this.rows = rows;
        rotation = rotationSystem;
    }

    public int Count { get; private set; }

    public Block Spawn(Shape shape) => spawns[(int)shape];

    public Block Select(int col, int offset, Shape shape, Rotation rotation = default)
       => Select(col, offset, shape, rotation, 0);

    public Block Select(int col, int offset, Shape shape, Rotation rotation, int add)
        => items[col][offset][(int)shape][rotation.Rotate(add).Int()];

    public IEnumerator<Block> GetEnumerator() => items
        .SelectMany(m => m)
        .SelectMany(n => n)
        .SelectMany(o => o)
        .Where(b => b is { })
        .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Block[] spawns;
    private readonly Rows rows;
    private readonly RotationSystem rotation;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Block[][][][] items = Jagged.Array<Block>(10, 20, 7, 4);
}
