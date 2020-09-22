namespace Tetris
{
    public partial class Shape
    {
        public static readonly Shape S = New(ShapeType.S, default,
            0b011,
            0b110,
            0b000);

        public static readonly Shape S_R = New(ShapeType.S, Rotation.Right,
            0b010,
            0b011,
            0b001);

        public static readonly Shape S_U = New(ShapeType.S, Rotation.Uturn,
            0b000,
            0b011,
            0b110);

        public static readonly Shape S_L = New(ShapeType.S, Rotation.Left,
            0b100,
            0b110,
            0b010);
    }
}
