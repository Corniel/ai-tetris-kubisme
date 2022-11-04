namespace Block_specs;

public class Initialized
{
    static readonly Blocks blocks = Blocks.Init();

    [Test]
    public void For_JLOT_IDs_and_Primaries_are_equal()
    {
        var blocks = Blocks.Init();

        var JLOT = blocks.Where(b
            => b.Shape == Shape.J
            || b.Shape == Shape.L
            || b.Shape == Shape.O
            || b.Shape == Shape.T);

        JLOT.Should().AllSatisfy(block => block.Primary.Should().Be(block.Id, because: block.ToString()));
    }

    [Test]
    public void Within_50ms()
    {
        var rows = Rows.All();
        Speed.Runs(() => Blocks.Init(rows, null), 1, TimeSpan.FromMilliseconds(50));
    }

    [Test]
    public void Results_in_3736_blocks()
    {
        var all = blocks.ToArray();

        all.Should().HaveCount(3736);

        var rotations = all.Where(b => b.TurnRight != null && b.TurnLeft != null).ToArray();

        var roundtripL = rotations.Where(b => b.TurnLeft?.TurnLeft?.TurnLeft?.TurnLeft == b).ToArray();
        var roundtripR = rotations.Where(b => b.TurnRight?.TurnRight?.TurnRight?.TurnRight == b).ToArray();

        rotations.Except(roundtripL).Except(roundtripR).Should().BeEmpty();
    }
}
