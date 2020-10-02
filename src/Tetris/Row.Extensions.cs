using System.Linq;

namespace Tetris
{
    public static class RowExtensions
    {
        public static int Count(this Row[] rows)
            => rows.Sum(row => row.Count);

        public static int Width(this Row[] rows)
        {
            var merged = rows[0];
            for (var i = 1; i < rows.Length; i++)
            {
                merged |= rows[i];
            }
            return merged.Count;
        }

        public static Row[] Right(this Row[] rows)
        {
            var copy = new Row[rows.Length];
            for(var i = 0; i < copy.Length; i++)
            {
                copy[i] = rows[i].Right();
            }
            return copy;
        }
    }
}
