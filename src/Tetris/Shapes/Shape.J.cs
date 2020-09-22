namespace Tetris
{
    public partial class Shape
    {
        public static readonly Shape J = New(ShapeType.J, default,
            0b100,
            0b111,
            0b000);

        public static readonly Shape J_R = New(ShapeType.J, Rotation.Right,
            0b011,
            0b010,
            0b010);

        public static readonly Shape J_U = New(ShapeType.J, Rotation.Uturn,
            0b000,
            0b111,
            0b001);

        public static readonly Shape J_L = New(ShapeType.J, Rotation.Left,
            0b010,
            0b010,
            0b110);
    }
}
