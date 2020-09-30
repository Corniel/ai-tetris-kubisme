using NUnit.Framework;
using System;
using System.Linq;
using Tetris;
using Tetris.Kubisme.Generation;

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

            var generator = new MoveGenerator(field, blocks.Spawn(shape));
            var all = generator.ToArray();

            Assert.AreEqual(moves, all.Length);
        }
    }
}
