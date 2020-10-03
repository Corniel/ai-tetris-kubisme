using System.Collections.Generic;

namespace Tetris
{
    public enum Rotation : byte
    {
        None = 0,
        Right = 1,
        Uturn = 2,
        Left = 3,
    }

    public static class Rotations
    {
        public static readonly IReadOnlyList<Rotation> All = new[]
        {
            Rotation.None,
            Rotation.Right,
            Rotation.Uturn,
            Rotation.Left,
        };

        public static int Int(this Rotation rotation) => (int)rotation;

        public static Rotation Primary(this Rotation rotation)
            => (Rotation)(rotation.Int() & 1);

        public static Rotation Rotate(this Rotation rotation, int steps)
            => (Rotation)((rotation.Int() + steps) & 3);
    }

}
