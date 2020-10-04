using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using Testris.Specs;
using Tetris;
using Tetris.Generation;
using Troschuetz.Random.Generators;

namespace MoveGenerator_specs
{
    public class Finds_All
    {
        [TestCase(17, Shape.I)]
        [TestCase(34, Shape.J)]
        [TestCase(34, Shape.L)]
        [TestCase(09, Shape.O)]
        [TestCase(17, Shape.S)]
        [TestCase(34, Shape.T)]
        [TestCase(17, Shape.Z)]
        public void For_Start_field(int moves, Shape shape)
        {
            var blocks = Blocks.Init();
            var field = Field.Start;

            var all = Speed.Measure(()=>
            {
                var generator = MoveGenerator.New(field, blocks.Spawn(shape));
                return generator.ToArray();
            });

            foreach (var move in all)
            {
                Console.WriteLine(move.Steps);
            }
            Assert.AreEqual(moves, all.Length);
        }
    }

    public class Random_Access
    {
        /// <remarks>
        /// ca. 2.2M paths/second
        /// </remarks>
        [Test]
        public void Speed()
        {
            var count = 1_000_000;
            var blocks = Blocks.Init();
            var fields = new Field[count];

            var rnd = new MT19937Generator(17);

            for (var i = 0; i < count; i++)
            {
                var height = rnd.Next(1, 5) * rnd.Next(1, 5);
                var rows = new List<ushort>();
                for (var h = 0; h < height; h++)
                {
                    var bits = (ushort)(rnd.Next(1, 0b_11111_11111) & rnd.Next(1, 0b_11111_11111));
                    if (Row.New(bits).NotEmpty())
                    {
                        rows.Add(bits);
                    }
                }
                fields[i] = Field.New(rows.ToArray());
            }

            var moves = new List<MoveCandidate>(500);

            foreach (var shape in Shapes.All.Reverse())
            {
                Console.Write($"Shape: {shape}, ");
                Speed.Runs(() =>
                {
                    foreach (var field in fields)
                    {
                        moves.Clear();
                        var generator = MoveGenerator.New(field, blocks.Spawn(shape));
                        moves.AddRange(generator);
                        generator.Release();
                    }
                });
            }
        }
    }
}
