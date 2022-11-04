using System.Runtime;

namespace Tetris;

public readonly struct Row : IEquatable<Row>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ushort bits;

    public static readonly Row Empty;
    public static readonly Row Full = new(0b_11111_11111);

    internal Row(ushort b) => bits = b;

    public int Count => counts[bits];

    /// <summary>Returns true if empty.</summary>
    public bool IsEmpty() => bits == default;

    /// <summary>Returns true if not empty.</summary>
    public bool NotEmpty() => bits != default;

    /// <summary>Returns true if full.</summary>
    public bool IsFull() => bits == Full.bits;

    /// <summary>Moves the row one to the right.</summary>
    public Row Right() => new(unchecked((ushort)(bits >> 1)));

    /// <summary>Merges two rows.</summary>
    public Row Merge(Row other) => new(unchecked((ushort)(bits | other.bits)));

    /// <summary>Returns a row that represents an overlap.</summary>
    public Row Overlap(Row other) => new(unchecked((ushort)(bits & other.bits)));

    /// <summary>Return true if the rows have overlap.</summary>
    public bool HasOverlapWith(Row other) => (bits & other.bits) != 0;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Row other && Equals(other);

    /// <inheritdoc />
    public bool Equals(Row other) => bits == other.bits;

    /// <inheritdoc />
    public override int GetHashCode() => bits;

    /// <inheritdoc />
    public override string ToString()
    {
        var chs = new char[10];
        for (var i = 0; i < 10; i++)
        {
            chs[i] = (flag[i] & bits) == 0 ? '.' : 'X';
        }
        return new string(chs);
    }

    public static bool operator ==(Row l, Row r) => l.Equals(r);
    public static bool operator !=(Row l, Row r) => !(l == r);
    public static Row operator |(Row l, Row r) => l.Merge(r);

    public static Row New(ushort bits) => new(bits);

    internal static ushort Parse(string line)
    {
        ushort row = 0;
        var index = 0;
        
        foreach (var ch in line)
        {
            switch (ch)
            {
                case 'X': row |= flag[index++]; break;
                case '.': index++; break;
                default: /* ignore */ break;
            }
        }
        return row;
    }

    private static readonly ushort[] flag = new ushort[]
    {
        0b_10000_00000,
        0b_01000_00000,
        0b_00100_00000,
        0b_00010_00000,
        0b_00001_00000,
        0b_00000_10000,
        0b_00000_01000,
        0b_00000_00100,
        0b_00000_00010,
        0b_00000_00001,
        0, 0, 0, 0, 0, 0
    };

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static readonly byte[] counts = CalcCount();

    private static byte[] CalcCount()
    {
        var lookup = new byte[Full.bits + 1];
        for (var row = Empty.bits; row <= Full.bits; row++)
        {
            uint b = row;
            b -= ((b >> 1) & 0x55555555);
            b = (b & 0x33333333) + ((b >> 2) & 0x33333333);
            var count = (((b + (b >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
            lookup[row] = (byte)count;
        }
        return lookup;
    }
}
