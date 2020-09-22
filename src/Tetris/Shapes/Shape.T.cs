namespace Tetris
{
    public partial class Shape
    {
        public static readonly Shape T = New(ShapeType.T, default,
            0b010,
            0b111,
            0b000);

        public static readonly Shape T_R = New(ShapeType.T, Rotation.Right,
            0b010,
            0b011,
            0b010);

        public static readonly Shape T_U = New(ShapeType.T, Rotation.Uturn,
            0b000,
            0b111,
            0b010);

        public static readonly Shape T_L = New(ShapeType.T, Rotation.Left,
            0b010,
            0b110,
            0b010);
    }
}
