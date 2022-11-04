namespace Tetris;

public readonly struct Move
{
    public readonly Clearing Clearing;
    public readonly Field Field;

    public Move(Clearing clearing, Field field)
    {
        Clearing = clearing;
        Field = field;
    }
}
