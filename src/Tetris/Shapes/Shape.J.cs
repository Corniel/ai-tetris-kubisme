namespace Tetris
{
    public partial class Shape
    {
        public static readonly Shape J = New(ShapeType.J, default,
            0b100,
            0b111);

        public static readonly Shape J_R = New(ShapeType.J, Rotation.Right,
            0b11,
            0b10,
            0b10);

        public static readonly Shape J_U = New(ShapeType.J, Rotation.Uturn,
            0b111,
            0b001);

        public static readonly Shape J_L = New(ShapeType.J, Rotation.Left,
            0b01,
            0b01,
            0b11);
    }
}
