using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using Tetris;

namespace Blocks_specs
{
    public class Init
    {
        [Test]
        public void Default()
        {
            var sw = Stopwatch.StartNew();
            var blocks = Blocks.Init();
            sw.Stop();

            var all = blocks.ToArray();

            Assert.AreEqual(3736, all.Length);

            var rotations = all.Where(b => b.TurnRight != null && b.TurnLeft != null).ToArray();

            var roundtripL = rotations.Where(b => b.TurnLeft?.TurnLeft?.TurnLeft?.TurnLeft == b).ToArray();
            var roundtripR = rotations.Where(b => b.TurnRight?.TurnRight?.TurnRight?.TurnRight == b).ToArray();

            var mssing = rotations.Except(roundtripL).Except(roundtripR).ToArray();

            Assert.AreEqual(0, mssing.Length);

            Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            Assert.IsTrue(sw.Elapsed.TotalMilliseconds < 30, sw.Elapsed.TotalMilliseconds.ToString());
        }
    }
}
