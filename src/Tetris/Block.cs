using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    public sealed partial class Block : Rows
    {
        private readonly Row[] rows;

        private Block(Shape shape, int column, int offset)
            : this(shape, shape.ToArray(), column, offset) { }

        private Block(Shape shape, Row[] rs, int column, int offset)
        {
            Shape =shape;
            rows = rs;
            Column = column;
            Offset = offset;
            Height = rows.Length + offset;
            Id = new BlockId(Shape.Type, Shape.Rotation, Column, Offset);
        }

        public Row this[int row] => rows[row - Offset];

        public BlockId Id { get; }

        public Shape Shape { get; }

        public int Column { get; }
        public int Height { get; }
        public int Offset { get; }

        public Block Down { get; private set; }
        public Block Left { get; private set; }
        public Block Right { get; private set; }
        public Block TurnLeft { get; private set; }
        public Block TurnRight { get; private set; }

        /// <inheritdoc />
        public override string ToString() => Id.ToString();

        /// <inheritdoc />
        public IEnumerator<Row> GetEnumerator() => ((IEnumerable<Row>)rows).GetEnumerator();
        
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
