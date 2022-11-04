namespace Tetris;

internal readonly struct Offset
{
    public Offset(int column, int floor)
    {
        Column = column;
        Floor = floor;
    }

    public int Column { get; }
    public int Floor { get; }

    public override string ToString() => $"Col: {Column}, Floor: {Floor}";
}
