using SmartAss;
using System.Collections.Generic;
using Tetris;
using Troschuetz.Random.Generators;

namespace Testris.Specs
{
    public static class Data
    {
        public static Field[] Fields(int count = 1000)
        {
            var fields = new Field[count];
            var rnd = new MT19937Generator(count);

            for (var i = 0; i < count; i++)
            {
                var height = rnd.Next(1, 5) * rnd.Next(1, 5);
                var rows = new List<ushort>(20);
                for (var h = 0; h < height; h++)
                {
                    var bits = (ushort)(rnd.Next(1, 0b_11111_11111) & rnd.Next(1, 0b_11111_11111));
                    if (Row.New(bits).NotEmpty())
                    {
                        rows.Add(bits);
                    }
                }
                while(rows.Count < 20)
                {
                    rows.Insert(0, default);
                }

                fields[i] = Field.New(rows.ToArray());
            }

            return fields;
        }

        public static Block Block(Shape shape, Rotation rotation, int column, int offset, params ushort[] rows)
            => new TestBlock(Rows.New(rows), shape, rotation, column, offset);

        private class TestBlock : Block
        {
            public TestBlock(Row[] rows, Shape shape, Rotation rotation, int column, int offset)
                : base(rows, shape, rotation, column, offset) => Do.Nothing();
        }
    }

}
