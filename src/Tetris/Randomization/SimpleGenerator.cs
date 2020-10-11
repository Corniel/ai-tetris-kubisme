using SmartAss;
using Troschuetz.Random;
using Troschuetz.Random.Generators;

namespace Tetris.Randomization
{
    public class SimpleGenerator : NextShape
    {
        public SimpleGenerator() : this(null) => Do.Nothing();

        public SimpleGenerator(int seed)
            : this(new MT19937Generator(seed)) => Do.Nothing();

        public SimpleGenerator(IGenerator generator)
            => Rnd = generator ?? new MT19937Generator();

        protected IGenerator Rnd { get; }

        public virtual Shape Next() => (Shape)Rnd.Next(8);
    }
}
