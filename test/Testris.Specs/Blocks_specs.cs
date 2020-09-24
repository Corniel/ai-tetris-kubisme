using NUnit.Framework;
using System.Linq;
using Tetris;

namespace Blocks_specs
{
    public class Init
    {
        [Test]
        public void Default()
        {
            var blocks = Blocks.Init();

            var all = blocks.ToArray();

            Assert.AreEqual(3931, all.Length);

            var rotations = all.Where(b => b.TurnRight != null && b.TurnLeft != null).ToArray();

            var roundtripL = rotations.Where(b => b.TurnLeft?.TurnLeft?.TurnLeft?.TurnLeft == b).ToArray();
            var roundtripR = rotations.Where(b => b.TurnRight?.TurnRight?.TurnRight?.TurnRight == b).ToArray();

            var mssing = rotations.Except(roundtripL).ToArray();

            var i = blocks.Spawn(ShapeType.I);

            var expected = Field.New(
                0b_00011_11000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000);

            Assert.AreEqual(expected, Field.Start.Move(i).Field);
        }
    }
}
