namespace Tetris
{
    public partial class Shape
    {
        public static readonly Shape L = New(ShapeType.L, default,
            0b001,
            0b111,
            0b000);

        public static readonly Shape L_R = New(ShapeType.L, Rotation.Right,
            0b010,
            0b010,
            0b011);

        public static readonly Shape L_U = New(ShapeType.L, Rotation.Uturn,
            0b000,
            0b111,
            0b100);

        public static readonly Shape L_L = New(ShapeType.L, Rotation.Left,
            0b110,
            0b010,
            0b010);
    }
}
