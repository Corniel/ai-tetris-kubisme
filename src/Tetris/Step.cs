﻿namespace Tetris;

public enum Step
{
    None = 0,
    Down = 1,
    Left = 2,
    Right = 3,
    TurnLeft = 4,
    TurnRight = 5,
    Drop = 6,
    Skip = 7,
}

public static class StepExtensions
{
    public static bool IsRotation(this Step step) 
        => step == Step.TurnLeft
        || step == Step.TurnRight;
}
