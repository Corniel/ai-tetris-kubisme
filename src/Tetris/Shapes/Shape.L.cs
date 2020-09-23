namespace Tetris
{
    public partial class Shape
    {
        public static readonly Shape L = New(ShapeType.L, default,
            0b001,
            0b111);

        public static readonly Shape L_R = New(ShapeType.L, Rotation.Right,
            0b10,
            0b10,
            0b11);

        public static readonly Shape L_U = New(ShapeType.L, Rotation.Uturn,
            0b111,
            0b100);

        public static readonly Shape L_L = New(ShapeType.L, Rotation.Left,
            0b11,
            0b01,
            0b01);
    }
}
