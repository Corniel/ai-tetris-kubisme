namespace Tetris
{
    public partial class Shape
    {
        public static readonly Shape I = New(ShapeType.I, default,
            0b1111);

        public static readonly Shape I_R = New(ShapeType.I, Rotation.Right,
            0b1,
            0b1,
            0b1,
            0b1);

        public static readonly Shape I_U = New(ShapeType.I, Rotation.Uturn,
           0b1111);

        public static readonly Shape I_L = New(ShapeType.I, Rotation.Left,
            0b1,
            0b1,
            0b1,
            0b1);
    }
}
