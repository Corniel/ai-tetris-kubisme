namespace Tetris
{
    public partial class Shape
    {
        public static readonly Shape I = New(ShapeType.I, default,
            0b0000,
            0b1111,
            0b0000,
            0b0000);

        public static readonly Shape I_R = New(ShapeType.I, Rotation.Right,
            0b0010,
            0b0010,
            0b0010,
            0b0010);
        
        public static readonly Shape I_U = New(ShapeType.I, Rotation.Uturn,
            0b0000,
            0b0000,
            0b1111,
            0b0000);
        
        public static readonly Shape I_L = New(ShapeType.I, Rotation.Left,
            0b0100,
            0b0100,
            0b0100,
            0b0100);
    }
}
