namespace FluentAssertions;

public sealed class FieldAssertions
{
    private readonly Field Field;

    public FieldAssertions(Field field) => Field = field;

    public void Be(string expected) => Be(Field.Parse(expected));

    public void Be(Field expected)
    {
        Field.Height.Should().Be(expected.Height);

        Field.ToString().Split("\r\n")
            .Should().BeEquivalentTo(expected.ToString().Split("\r\n"));
    }

}
