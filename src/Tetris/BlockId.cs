using System;
using System.Diagnostics;

namespace Tetris
{
    public readonly struct BlockId : IEquatable<BlockId>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int id;

        public BlockId(ShapeType shape, Rotation rotation, int column, int offset)
        {
            id = (int)shape;
            id *= 4;
            id += (int)rotation;
            id *= 12;
            id += column +2;
            id *= 20;
            id += offset;
        }

        public ShapeType Shape => (ShapeType)(id / (20 * 12 * 4));
        public Rotation Rotation => (Rotation)(id / (20 * 12) % 4);
        public int Column => (id / 20) % 12 - 2;
        public int Offset => id % 20;

        /// <inheritdoc />
        public override string ToString() => $"Shape: {Shape}{(Rotation == default ? "" : $" ({Rotation})")}, Col: {Column}, Offset: {Offset}";

        /// <inheritdoc />
        public override int GetHashCode() => id;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is BlockId other && Equals(other);

        /// <inheritdoc />
        public bool Equals(BlockId other) => id == other.id;
    }
}
