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
                    rows.Add(default);
                }

                fields[i] = Field.New(rows.ToArray());
            }

            return fields;
        }
    }
}
