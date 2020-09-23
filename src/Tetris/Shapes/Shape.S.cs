namespace Tetris
{
    public partial class Shape
    {
        public static readonly Shape S = New(ShapeType.S, default,
            0b011,
            0b110);

        public static readonly Shape S_R = New(ShapeType.S, Rotation.Right,
            0b10,
            0b11,
            0b01);

        public static readonly Shape S_U = New(ShapeType.S, Rotation.Uturn,
            0b011,
            0b110);

        public static readonly Shape S_L = New(ShapeType.S, Rotation.Left,
            0b10,
            0b11,
            0b01);
    }
}
