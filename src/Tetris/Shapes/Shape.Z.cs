namespace Tetris
{
    public partial class Shape
    {
        public static readonly Shape Z = New(ShapeType.Z, default,
            0b110,
            0b011);

        public static readonly Shape Z_R = New(ShapeType.Z, Rotation.Right,
            0b01,
            0b11,
            0b10);

        public static readonly Shape Z_U = New(ShapeType.Z, Rotation.Uturn,
            0b110,
            0b011);

        public static readonly Shape Z_L = New(ShapeType.Z, Rotation.Left,
            0b01,
            0b11,
            0b10);
    }
}
