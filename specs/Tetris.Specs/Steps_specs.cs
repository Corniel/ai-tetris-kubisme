namespace Steps_specs;

public class Create
{
    [Test]
    public void From_array_of_5_contains_full_input()
    {
        var array = new [] { Step.Left, Step.Right, Step.Down, Step.TurnLeft, Step.TurnRight };
        var steps = Steps.Create(array);

        steps.Count.Should().Be(5);
        steps.ToArray().Should().BeEquivalentTo(array);
        steps.ToString().Should().Be("left,right,down,turnleft,turnright");
    }

    [Test]
    public void From_array_of_25_contains_full_input()
    {
        var array = new[]
        {
            Step.Left, Step.Right, Step.Down, Step.TurnLeft, Step.TurnRight,
            Step.Left, Step.Right, Step.Down, Step.TurnLeft, Step.TurnRight,
            Step.Left, Step.Right, Step.Down, Step.TurnLeft, Step.TurnRight,
            Step.Left, Step.Right, Step.Down, Step.TurnLeft, Step.TurnRight,
            Step.Left, Step.Right, Step.Down, Step.TurnRight, Step.TurnRight 
        };

        var steps = Steps.Create(array);

        steps.Count.Should().Be(25);
        steps.ToArray().Should().BeEquivalentTo(array);
        steps.ToString().Should().Be(
            "left,right,down,turnleft,turnright," +
            "left,right,down,turnleft,turnright," +
            "left,right,down,turnleft,turnright," +
            "left,right,down,turnleft,turnright," +
            "left,right,down,turnright,turnright");
    }

    [Test]
    public void Down_zero()
    {
        var steps = Steps.Down(0);
        steps.Count.Should().Be(0);
        steps.Count().Should().Be(0);
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(9)]
    [TestCase(10)]
    [TestCase(11)]
    [TestCase(12)]
    [TestCase(13)]
    [TestCase(14)]
    [TestCase(15)]
    [TestCase(16)]
    [TestCase(17)]
    [TestCase(18)]
    [TestCase(19)]
    [TestCase(20)]
    public void Down(int st)
    {
        var steps = Steps.Down(st);
        steps.Count.Should().Be(st);
        steps.Count().Should().Be(st);
        steps.Should().AllSatisfy(step => step.Should().Be(Step.Down));
    }
}
