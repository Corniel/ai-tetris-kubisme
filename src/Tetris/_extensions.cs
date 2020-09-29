using System.Linq;

namespace Tetris
{
    public static class RowExtensions
    {
        public static int Width(this Row[] rows)
        {
            var merged = rows[0];
            for (var i = 1; i < rows.Length; i++)
            {
                merged |= rows[i];
            }
            return merged.Count;
        }

        public static Row[] Right(this Row[] rows) => rows.Select(row => row.Right()).ToArray();
    }
}
