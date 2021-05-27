using SmartAss;
using System.Diagnostics;

namespace Tetris
{
    public partial class Block
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Row[] rows;

        protected Block(Row[] rows, Shape shape, Rotation rotation, int column, int offset)
        {
            this.rows = rows;
            Shape = shape;
            Rotation = rotation;
            Column = column;
            Offset = offset;
            HasFloor = offset == 0;
        }

        internal Block(Rows rows, Shape shape, Rotation rotation, int column, int offset)
            : this(rows.Select(shape, rotation, column), shape, rotation, column, offset) => Do.Nothing();
        
        /// <summary>Gets the identifier of the block.</summary>
        public short Id { get; internal set; }

        /// <summary>Gets the primary (identifier) of the block.</summary>
        public short Primary { get; internal set; }

        /// <summary>Gets <see cref="Tetris.Shape"/> of the block.</summary>
        public Shape Shape { get; }

        /// <summary>Gets <see cref="Tetris.Rotation"/> of the block.</summary>
        public Rotation Rotation { get; }

        /// <summary>Gets the column (offset).</summary>
        internal int Column { get; }

        /// <summary>True if we deal with a T-spin.</summary>
        public virtual bool TSpin(Row[] rows) => false;

        /// <inheritdoc />
        public override string ToString()
        => $"Shape: {Shape}{(Rotation == default ? "" : $" ({Rotation})")}, " +
            $"Col: {Column}, Offset: {Offset}";
    }
}
