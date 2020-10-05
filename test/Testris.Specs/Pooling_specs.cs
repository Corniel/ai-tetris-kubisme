using NUnit.Framework;
using System.Linq;
using Tetris;
using Tetris.Generation;

namespace Pooling_specs
{
    public class MoveGenerator_Pooling
    {
        [Test]
        public void Release_allows_generator_to_be_reused()
        {
            var blocks = Blocks.Init();
            var field = Field.Start;

            var gen0 = MoveGenerator.New(field, blocks.Spawn(Shape.S));

            var first = gen0.ToArray();

            gen0.Release();

            var gen1 = MoveGenerator.New(field, blocks.Spawn(Shape.S));

            var second = gen1.ToArray();

            Assert.AreSame(gen0, gen1);
            Assert.AreEqual(first, second);


        }
    }
}
