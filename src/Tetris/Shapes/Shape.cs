using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    public sealed partial class Shape : Rows
    {
        public static IReadOnlyCollection<Shape> All  => new Shape[]
        {
            I, I_R, I_U, I_L,
            J, J_R, J_U, J_L,
            L, L_R, L_U, L_L,
            O,
            S, S_R, S_U, S_L,
            T, T_R, T_U, T_L,
            Z, I_R, Z_U, Z_L,
        };

        private readonly Row[] rows;

        private Shape(
            ShapeType type,
            Rotation rotation,
            int height,
            int width,
            Row[] rows)
        {
            Type = type;
            Rotation = rotation;
            Height = height;
            Width = width;
            this.rows = rows;
        }

        public Row this[int row] => rows[row];

        /// <summary>Get the count of the bits.</summary>
        public int Count => rows.Sum(row => row.Count);

        public ShapeType Type { get; }
        public Rotation Rotation { get; }

        /// <summary>Gets the height of the shape.</summary>
        public int Height { get; }

        /// <summary>Gets the width of the shape.</summary>
        public int Width { get; }

        /// <inheritdoc />
        public override string ToString()
        => string.Join(
            '|',
            rows.Reverse().Select(row => row.ToString().Substring(10 - Width)));

        internal Row[] ToArray() => rows;

        private static Shape New(
            ShapeType type,
            Rotation rotation,
            params ushort[] lines)
        {
            var rows = lines.Select(row => new Row(row)).Reverse().ToArray();
            var merged = rows[0];
            var height = rows.Length;

            foreach (var r in rows) { merged |= r; }

            var width = merged.Count;

            return new Shape(type, rotation, height, width, rows);
        }
    }
}
