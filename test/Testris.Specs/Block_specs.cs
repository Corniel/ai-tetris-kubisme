using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Testris.Specs;
using Tetris;

namespace Block_specs
{
    public class Initialized
    {
        [Test]
        public void For_JLOT_IDs_and_Primaries_are_equal()
        {
            var blocks = Blocks.Init();

            var JLOT = blocks.Where(b
                => b.Shape == Shape.J
                || b.Shape == Shape.L
                || b.Shape == Shape.O
                || b.Shape == Shape.T);

            foreach(var block in JLOT)
            {
                Assert.AreEqual(block.Id, block.Primary, block.ToString());
            }
        }
        [TestCase(Shape.I)]
        [TestCase(Shape.S)]
        [TestCase(Shape.Z)]
        public void Primaries_occur_not_more_then_twice_for(Shape shape)
        {
            var blocks = Blocks.Init();

            var subset = blocks.Where(b => b.Shape == shape);

            var lookup = new Dictionary<int, int>();

            foreach (var block in subset)
            {
                if(lookup.ContainsKey(block.Primary))
                {
                    lookup[block.Primary]++;
                }
                else
                {
                    lookup[block.Primary] = 1;
                }
            }

            Assert.IsTrue(lookup.All(kvp => kvp.Value < 3));
        }

        [Test]
        public void Within_50ms()
        {
            var rows = Rows.All();
            Speed.Runs(() => Blocks.Init(rows, null), TimeSpan.FromMilliseconds(50));
        }

        [Test]
        public void Results_in_3736_blocks()
        {
            var blocks = Blocks.Init();
            var all = blocks.ToArray();

            Assert.AreEqual(3736, all.Length);

            var rotations = all.Where(b => b.TurnRight != null && b.TurnLeft != null).ToArray();

            var roundtripL = rotations.Where(b => b.TurnLeft?.TurnLeft?.TurnLeft?.TurnLeft == b).ToArray();
            var roundtripR = rotations.Where(b => b.TurnRight?.TurnRight?.TurnRight?.TurnRight == b).ToArray();

            var mssing = rotations.Except(roundtripL).Except(roundtripR).ToArray();

            Assert.AreEqual(0, mssing.Length);
        }
    }
}
