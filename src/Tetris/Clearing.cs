namespace Tetris;

public readonly struct Clearing : IEquatable<Clearing>
{
    public static readonly Clearing None;

    public static Clearing Perfect (int cleared) => new(cleared | Mask.perfect);
    
    public static Clearing TSpin(int cleared) => new(cleared | Mask.T_spin);

    public Clearing(int bits) => Bits = bits;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int Bits;

    /// <summary>The numbers of rows cleared.</summary>
    public int Rows => Bits & 0b0000_0111;

    /// <summary>True if the remaining field is empty.</summary>
    public bool IsPerfect => (Bits & Mask.perfect) != 0;

    /// <summary>True if the clearing involved a T-spin.</summary>
    public bool IsTSpin => (Bits & Mask.T_spin) != 0;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Clearing other && Equals(other);

    /// <inheritdoc />
    public bool Equals(Clearing other) => Bits == other.Bits;

    /// <inheritdoc />
    public override int GetHashCode() => Bits;

    /// <inheritdoc />
    public override string ToString()
    {
        if (IsTSpin) return $"Rows: {Rows} (T-Spin)";
        else return $"Rows: {Rows}{(IsPerfect ? "*" : "")}";
    }

    private static class Mask
    {
        public const byte perfect = 0b0001_0000;
        public const byte T_spin = 0b00000_1000;
    }
}
