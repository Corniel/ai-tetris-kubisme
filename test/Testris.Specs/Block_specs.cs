using NUnit.Framework;
using System;
using System.Linq;
using Testris.Specs;
using Tetris;

namespace Block_specs
{
    public class Initialized
    {
        [Test]
        public void Within_10ms()
        {
            var rows = Rows.All();
            Speed.Runs(() => Blocks.Init(rows, null), 1, TimeSpan.FromMilliseconds(10));
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
