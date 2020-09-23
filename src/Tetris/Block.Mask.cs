using System.Linq;

namespace Tetris
{
    public partial class Block
    {
        /// <summary>Gets a row based on the (zero based) row number.</summary>
        public Row this[int row] => rows[row - Offset];
        
        /// <summary>Gets the offset from the floor.</summary>
        public int Offset { get; }

        /// <summary>Gets the height of the block (including <see cref="Offset"/>.</summary>
        public int Height => Offset + Shape.Height;
    }
}
