using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    public sealed partial class Shape : Rows
    {
        private readonly Row[] rows;

        private Shape(
            ShapeType type,
            Rotation rotation,
            int height,
            int width,
            int left,
            int top,
            Row[] rs)
        {
            Type = type;
            Rotation = rotation;
            Height = height;
            Width = width;
            Left = left;
            Top = top;
            rows = rs;
        }

        public Row this[int row] => rows[row];

        /// <summary>Get the count of the bits.</summary>
        public int Count => rows.Sum(row => row.Count);

        public ShapeType Type { get; }
        public Rotation Rotation { get; }

        public int Height { get; }
        public int Width { get; }

        public int Left { get; }
        public int Top { get; }



        /// <inheritdoc />
        public override string ToString()
        => string.Join(
            '|',
            rows.Reverse().Select(row => row.ToString().Substring(10 - Left - Width)));

        private static Shape New(
            ShapeType type,
            Rotation rotation,
            params ushort[] rs)
        {
            var rows = rs.Select(r => new Row(r)).Where(r => r.NotEmpty()).Reverse().ToArray();
            var merged = rs[0];
            var height = rows.Length;
            var top = rs.TakeWhile(r => r == 0).Count();

            foreach (var r in rs) { merged |= r; }

            var left = 0;
            var width = 0;

            switch (merged)
            {
                case 2:
                    left = 1;
                    width = 1;
                    break;

                case 3:
                    left = 0;
                    width = 2;
                    break;

                case 4:
                    left = 2;
                    width = 1;
                    break;

                case 6:
                    left = 1;
                    width = 2;
                    break;

                case 7:
                    left = 0;
                    width = 3;
                    break;

                case 15:
                    left = 0;
                    width = 4;
                    break;

                default: throw new NotSupportedException($"Type: {type}, Rotation: {rotation}, Merged: {merged}.");
            }
            return new Shape(type, rotation, height, width, left, top, rows);
        }

        /// <inheritdoc />
        public IEnumerator<Row> GetEnumerator() => ((IEnumerable<Row>)rows).GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
