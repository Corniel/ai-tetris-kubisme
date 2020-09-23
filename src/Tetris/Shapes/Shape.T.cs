namespace Tetris
{
    public partial class Shape
    {
        public static readonly Shape T = New(ShapeType.T, default,
            0b010,
            0b111);

        public static readonly Shape T_R = New(ShapeType.T, Rotation.Right,
            0b10,
            0b11,
            0b10);

        public static readonly Shape T_U = New(ShapeType.T, Rotation.Uturn,
            0b111,
            0b010);

        public static readonly Shape T_L = New(ShapeType.T, Rotation.Left,
            0b01,
            0b11,
            0b01);
    }
}
