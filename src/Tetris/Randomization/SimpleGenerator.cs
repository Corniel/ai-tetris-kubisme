using MathNet.Numerics.Random;

namespace Tetris.Randomization;

public class SimpleGenerator : NextShape
{
    public SimpleGenerator() : this(null) { }

    public SimpleGenerator(int seed)
        : this(new MersenneTwister(seed)) { }

    public SimpleGenerator(Random? generator)
        => Rnd = generator ?? new MersenneTwister();

    protected Random Rnd { get; }

    public virtual Shape Next() => (Shape)Rnd.Next(8);
}
