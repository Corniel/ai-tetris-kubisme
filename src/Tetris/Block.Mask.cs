namespace Tetris
{
    public partial class Block
    {
        /// <summary>Gets a row based on the (zero based) row number.</summary>
        public Row this[int row] => rows[row - Offset];
        
        /// <summary>Gets the offset from the floor.</summary>
        public int Offset { get; }

        /// <summary>True if the offset is 0.</summary>
        public bool HasFloor { get; }

        /// <summary>Gets the height of the block (including <see cref="Offset"/>.</summary>
        public int Height => Offset + rows.Length;
    }
}
