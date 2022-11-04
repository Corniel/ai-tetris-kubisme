namespace Clearing_specs;

public class Ctor
{
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public void Sets_rows_cleared(int rows)
    {
        var clearing = new Clearing(rows);
        clearing.IsTSpin.Should().BeFalse();
        clearing.IsPerfect.Should().BeFalse();
        clearing.Rows.Should().Be(rows);
    }
}

public class WithTSpin
{
    [Test]
    public void Is_toggled()
    {
        var clearing = Clearing.TSpin(2);
        clearing.IsTSpin.Should().BeTrue();
        clearing.Rows.Should().Be(2);
    }
}

public class ToString
{
    [Test]
    public void Shows_rows()
    {
        var clearing = new Clearing(4);
        clearing.ToString().Should().Be("Rows: 4");
    }

    [Test]
    public void Shows_Perfect()
    {
        var clearing = Clearing.Perfect(4);
        clearing.ToString().Should().Be("Rows: 4*");
    }

    [Test]
    public void Shows_TSpin()
    {
        var clearing = Clearing.TSpin(1);
        clearing.ToString().Should().Be("Rows: 1 (T-Spin)");
    }
}

