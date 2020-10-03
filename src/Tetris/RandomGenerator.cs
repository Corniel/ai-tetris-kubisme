using SmartAss;
using System.Linq;
using Troschuetz.Random;
using Troschuetz.Random.Generators;

namespace Tetris
{
    public class RandomGenerator
    {
        public RandomGenerator() : this(null) => Do.Nothing();

        public RandomGenerator(IGenerator generator)
            => Rnd = generator ?? new MT19937Generator();

        private IGenerator Rnd { get; }

        public Shape Next()
        => index > 6
            ? Shuffle()
            : bag[index++];

        private Shape Shuffle()
        {
            for (var source = 0; source < 7; source++)
            {
                var buffer = bag[source];
                var target = Rnd.Next(7);
                bag[source] = bag[target];
                bag[target] = buffer;
            }

            index = 1;
            return bag[0];
        }

        private readonly Shape[] bag = Shapes.All.ToArray();
        private int index = 7;
    }
}
