using NUnit.Framework;
using System;
using System.Collections.Generic;
using Testris.Specs;
using Tetris;
using Tetris.Generation;

namespace Field_specs
{
    public class Random_Access
    {
        /// <remarks>
        /// ca. 11.5M Moves/second
        /// </remarks>
        [Test]
        public void Move_block()
        {
            var count = 10_000;
            var blocks = Blocks.Init();
            var fields = Data.Fields(count);

            var moves = new List<MoveCandidate>[count];

            foreach (var shape in Shapes.All)
            {
                for (var i = 0; i < count; i++)
                {
                    var field = fields[i];

                    var generator = MoveGenerator.New(field, blocks.Spawn(shape));
                    moves[i] ??= new List<MoveCandidate>();
                    moves[i].AddRange(generator);
                }
            }

            var total = 0;

            var duration = Speed.Runs(() =>
            {
                for (var i = 0; i < count; i++)
                {
                    var field = fields[i];
                    var candidates = moves[i];

                    foreach (var candidate in candidates)
                    {
                        var move = field.Move(candidate.Block);
                        total++;
                    }
                }
            });

            Console.WriteLine($"total: {total:#,##0} ({total / (1000 * duration.TotalMilliseconds):#,##0.000} M/s)");
        }
    }
}
