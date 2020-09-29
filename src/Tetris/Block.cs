using System.Diagnostics;

namespace Tetris
{
    public partial class Block
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Row[] rows;

        internal Block(Rows rows, Shape shape, Rotation rotation, int column, int offset)
        {
            this.rows = rows.Select(shape, rotation, column);
            Shape = shape;
            Rotation = rotation;
            Column = column;
            Offset = offset;
        }

        /// <summary>Gets <see cref="Tetris.Shape"/> of the block.</summary>
        public Shape Shape { get; }

        /// <summary>Gets <see cref="Tetris.Rotation"/> of the block.</summary>
        public Rotation Rotation { get; }

        /// <summary>Gets the column (offset).</summary>
        internal int Column { get; }

        /// <inheritdoc />
        public override string ToString()
        => $"Shape: {Shape}{(Rotation == default ? "" : $" ({Rotation})")}, " +
            $"Col: {Column}, Offset: {Offset}";
    }
}
