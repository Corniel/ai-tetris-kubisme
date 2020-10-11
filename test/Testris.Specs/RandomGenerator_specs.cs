using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Randomization;

namespace RandomGenerator_specs
{
    public class Next_Returns_Bags_Of_7
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void So_all_shapes_occure_equaly(int bags)
        {
            var rnd = new RandomGenerator();

            var counts = new int[7];

            for (var i = 0; i < 7 * bags; i++)
            {
                counts[(int)rnd.Next()]++;
            }

            var exp = Enumerable.Repeat(bags, 7).ToArray();

            Assert.AreEqual(exp, counts);
        }

        [Test]
        public void With_random_distribution_within()
        {
            var rnd = new RandomGenerator();

            var variations = new Dictionary<string, int>();

            var sb = new StringBuilder();

            for (var i = 0; i < 100000; i++)
            {
                sb.Clear();

                for (var shape = 0; shape < 7; shape++)
                {
                    sb.Append(rnd.Next());
                }

                var variation = sb.ToString();

                if (variations.ContainsKey(variation))
                {
                    variations[variation]++;
                }
                else
                {
                    variations[variation] = 0;
                }
            }

            Assert.AreEqual(7 * 6 * 5 * 4 * 3 * 2, variations.Count);
        }
    }
}
