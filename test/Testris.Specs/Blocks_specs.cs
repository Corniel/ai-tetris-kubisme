using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            Assert.AreEqual(3769, all.Length);
        }
    }
}
