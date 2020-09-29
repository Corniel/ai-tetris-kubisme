using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tetris
{
    /// <summary>Represents a collection of <see cref="Row"/>s.</summary>
    public class Rows : IEnumerable<Row[]>
    {
        private Rows(Row[][][][] rs) => rows = rs;

        public static Row[] New(params ushort[] rows)
        {
            var copy = new Row[rows.Length];
            for(var i = 0; i < copy.Length; i++)
            {
                copy[i] = new Row(rows[copy.Length - 1 - i]);
            }
            return copy;
        }
        
        public static Row[] Trim(params ushort[] rows) => rows
           .Select(r => new Row(r))
           .Reverse()
           .SkipWhile(r => r.IsEmpty())
           .ToArray();

        public Block Block(Shape shape, Rotation rotation, int column, int offset)
            => offset < 0 || column < 0 || Columns(shape, rotation) <= column
            ? null
            : new Block(this, shape, rotation, column, offset);

        public int Columns(Shape shape, Rotation rotation)
            => rows[(int)shape][rotation].Length;

        public Row[] Select(Shape shape, Rotation rotation, int column)
            => rows[(int)shape][rotation][column];

        public static Rows All()
        {
            var rs = new Row[7][][][];

            var shapes = new[] 
            {
                new[]{ Shapes.I, Shapes.I_R, Shapes.I_U, Shapes.I_L },
                new[]{ Shapes.J, Shapes.J_R, Shapes.J_U, Shapes.J_L },
                new[]{ Shapes.L, Shapes.L_R, Shapes.L_U, Shapes.L_L },
                new[]{ Shapes.O },
                new[]{ Shapes.S, Shapes.S_R, Shapes.S_U, Shapes.S_L },
                new[]{ Shapes.T, Shapes.T_R, Shapes.T_U, Shapes.T_L },
                new[]{ Shapes.Z, Shapes.Z_R, Shapes.Z_U, Shapes.Z_L },
            };

            for(var shape = 0; shape < shapes.Length; shape++)
            {
                var rotations = shapes[shape].Length;
                rs[shape] = new Row[rotations][][];

                for(var rotation = 0; rotation < rotations; rotation++)
                {
                    var rows = shapes[shape][rotation];
                    var width = rows.Width();

                    var target = new Row[11 - width][];
                    var length = target.Length;
                    
                    target[0] = rows;

                    for (var i = 1; i < length; i++)
                    {
                        rows = rows.Right();
                        target[i] = rows;
                    }

                    rs[shape][rotation] = target;
                }
            }
            return new Rows(rs);
        }

        public IEnumerator<Row[]> GetEnumerator() => rows
            .SelectMany(m => m)
            .SelectMany(n => n)
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Row[][][][] rows;
    }
}

