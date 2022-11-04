using SmartAss;

namespace Tetris.Randomization;

public class RandomGenerator: SimpleGenerator
{
    public RandomGenerator() { }

    public RandomGenerator(int seed)
        : base(seed) { }

    public RandomGenerator(Random generator)
        : base(generator) { }

    public override Shape Next()
    => index > 6
        ? Shuffle()
        : bag[index++];

    public RandomGenerator SetBag(int index, params Shape[] known)
    {
        this.index = index;
        Array.Copy(known, bag, known.Length);
        var unkown = Shapes.All.Except(known).OrderBy(s => Rnd.Next()).ToArray();
        Array.Copy(
            sourceArray: unkown,
            destinationArray: bag,
            sourceIndex: 0,
            destinationIndex: known.Length,
            length: unkown.Length);

        return this;
    }

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
