using System.Runtime.CompilerServices;

namespace Tetris;

internal static class TSpin
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Is(Block block, IReadOnlyList<Row> rows)
    {
        if (block.Shape == Shape.T
           && block.Height < rows.Count)
        {
            var mask = Corners[block.Column];
            var corners = 
                mask.Overlap(rows[block.Height - 0]).Count +
                mask.Overlap(rows[block.Height - 2]).Count;

            return corners == 3;
        }
        else return false;
    }

    private static readonly Row[] Corners = new Row[]
{
        new(0b10100_00000),
        new(0b01010_00000),
        new(0b00101_00000),
        new(0b00010_10000),
        new(0b00001_01000),
        new(0b00000_10100),
        new(0b00000_01010),
        new(0b00000_00101),
};
}
