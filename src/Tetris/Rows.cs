using System.Collections.Generic;

namespace Tetris
{
    public interface Rows : IEnumerable<Row>
    {
        /// <summary>Gets the row of the specified row index.</summary>
        Row this[int row] { get; }

        /// <summary>Gets the total height of the rows.</summary>
        int Height { get; }
    }
}
