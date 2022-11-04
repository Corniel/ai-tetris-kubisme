namespace FluentAssertions;

public static class FluentExtensions
{
    [Pure]
    public static FieldAssertions Should(this Field field) => new(field);
}
