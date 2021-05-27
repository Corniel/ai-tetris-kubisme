using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Testris.Specs;
using Tetris;
using Tetris.Generation;

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

            var all = Speed.Measure(() =>
            {
                var generator = MoveGenerator.New(field, blocks.Spawn(shape));
                var result = generator.ToArray();
                return result;
            });

            foreach (var move in all)
            {
                Console.WriteLine(move.Path);
            }

            Assert.AreEqual(moves, all.Length);
        }
    }

    public class T_Spin
    {
        [Test]
        public void Found()
        {
            var field = Field.Parse(@"
                ..........                
                ..........
                ..........
                ...X..XX..
                XXXX...XXX
                XXXXX.XXXX");

            var spin = Path.Create(Step.Down, Step.TurnLeft, Step.Down, Step.Down, Step.Down, Step.TurnLeft);

            var paths = MoveGenerator.New(field, Blocks.Init(Rows.All(), 6, null).Spawn(Shape.T))
                .Select(m => m.Path)
                .ToArray();

            foreach(var path in paths)
            {
                Console.WriteLine(path);
            }

            Assert.That(paths.Length, Is.EqualTo(34 + 3));
            Assert.Contains(spin, paths);
        }
    }

    public class Random_Access
    {
        /// <remarks>
        /// ca. 1.0M paths/second
        /// </remarks>
        [Test]
        public void Generates_MoveCandidates()
        {
            var count = 10_000;
            var blocks = Blocks.Init();
            var fields = Data.Fields(count);

            var duration = TimeSpan.Zero;
            var total = 0;

            Assert.NotNull(MoveGenerator.New(Field.Start, blocks.Spawn(Shape.I)));

            foreach (var shape in Shapes.All)
            {
                Console.Write($"Shape: {shape}, ");

                duration += Speed.Runs(() =>
                {
                    foreach (var field in fields)
                    {
                        var generator = MoveGenerator.New(field, blocks.Spawn(shape));
                        total += generator.Count();
                        generator.Release();
                    }
                });
            }

            Console.WriteLine($"total: {total:#,##0} ({(total / 1000d) / duration.TotalMilliseconds:#,##0.000} M/s)");
        }
    }
}
