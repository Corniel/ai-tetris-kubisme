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
                generator.Release();
                return result;
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
        /// ca. 1.8M paths/second
        /// </remarks>
        [Test]
        public void Generates_MoveCandidates()
        {
            var count = 100_000;
            var blocks = Blocks.Init();
            var fields = Data.Fields(count);

            var moves = new List<MoveCandidate>(500);

            var duration = TimeSpan.Zero;
            var total = 0;

            foreach (var shape in Shapes.All)
            {
                Console.Write($"Shape: {shape}, ");

                duration += Speed.Runs(() =>
                {
                    foreach (var field in fields)
                    {
                        moves.Clear();
                        var generator = MoveGenerator.New(field, blocks.Spawn(shape));
                        moves.AddRange(generator);
                        total += moves.Count;
                        generator.Release();
                    }
                });
            }

            Console.WriteLine($"total: {total:#,##0} ({total / (1000 * duration.TotalMilliseconds):#,##0.000} M/s)");
        }
    }
}
