using SmartAss;
using System.Diagnostics;
using System.Linq;

namespace Tetris
{
    public partial class Block
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Row[] rows;

        internal Block(Shape shape)
            : this(shape.ToArray(), shape, 0, 0) => Do.Nothing();

        private Block(Row[] rows, Shape shape, int column, int offset)
        {
            this.rows = rows;
            Shape = shape;
            Column = column;
            Offset = offset;
        }

        /// <summary>Gets <see cref="Tetris.Shape"/> of the block.</summary>
        public Shape Shape { get; }

        /// <summary>Gets the column (offset).</summary>
        internal int Column { get; }

        /// <summary>Gets the width (including the column offset).</summary>
        internal int Width => Column + Shape.Width;

        /// <inheritdoc />
        public override string ToString()
        => $"Shape: {Shape.Type}{(Shape.Rotation == default ? "" : $" ({Shape.Rotation})")}, " +
            $"Col: {Column}, Offset: {Offset}";

        internal Block CreateUp() => new Block(rows, Shape, Column, Offset + 1);

        internal Block CreateRight() => new Block(rows.Select(r => r.Right()).ToArray(), Shape, Column + 1, Offset);
    }
}
