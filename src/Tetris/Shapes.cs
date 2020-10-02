namespace Tetris
{
    public static class Shapes
    {
        public static readonly Shape[] All = new[] 
        {
            Shape.I, 
            Shape.J,
            Shape.L, 
            Shape.O, 
            Shape.S,
            Shape.T, 
            Shape.Z 
        };

        public static readonly Row[] I = Rows.New(
            0b11110_00000);

        public static readonly Row[] I_R = Rows.New(
            0b10000_00000,
            0b10000_00000,
            0b10000_00000,
            0b10000_00000);

        public static readonly Row[] I_U = Rows.New(
            0b11110_00000);

        public static readonly Row[] I_L = Rows.New(
            0b10000_00000,
            0b10000_00000,
            0b10000_00000,
            0b10000_00000);

        public static readonly Row[] J = Rows.New(
            0b10000_00000,
            0b11100_00000);

        public static readonly Row[] J_R = Rows.New(
            0b11000_00000,
            0b10000_00000,
            0b10000_00000);

        public static readonly Row[] J_U = Rows.New(
            0b11100_00000,
            0b00100_00000);

        public static readonly Row[] J_L = Rows.New(
            0b01000_00000,
            0b01000_00000,
            0b11000_00000);

        public static readonly Row[] L = Rows.New(
            0b00100_00000,
            0b11100_00000);

        public static readonly Row[] L_R = Rows.New(
            0b10000_00000,
            0b10000_00000,
            0b11000_00000);

        public static readonly Row[] L_U = Rows.New(
            0b11100_00000,
            0b10000_00000);

        public static readonly Row[] L_L = Rows.New(
            0b11000_00000,
            0b01000_00000,
            0b01000_00000);

        public static readonly Row[] O = Rows.New(
            0b11000_00000,
            0b11000_00000);

        public static readonly Row[] S = Rows.New(
            0b01100_00000,
            0b11000_00000);

        public static readonly Row[] S_R = Rows.New(
            0b10000_00000,
            0b11000_00000,
            0b01000_00000);

        public static readonly Row[] S_U = Rows.New(
            0b01100_00000,
            0b11000_00000);

        public static readonly Row[] S_L = Rows.New(
            0b10000_00000,
            0b11000_00000,
            0b01000_00000);

        public static readonly Row[] T = Rows.New(
            0b01000_00000,
            0b11100_00000);

        public static readonly Row[] T_R = Rows.New(
            0b10000_00000,
            0b11000_00000,
            0b10000_00000);

        public static readonly Row[] T_U = Rows.New(
            0b11100_00000,
            0b01000_00000);

        public static readonly Row[] T_L = Rows.New(
            0b01000_00000,
            0b11000_00000,
            0b01000_00000);

        public static readonly Row[] Z = Rows.New(
            0b11000_00000,
            0b01100_00000);

        public static readonly Row[] Z_R = Rows.New(
            0b01000_00000,
            0b11000_00000,
            0b10000_00000);

        public static readonly Row[] Z_U = Rows.New(
            0b11000_00000,
            0b01100_00000);

        public static readonly Row[] Z_L = Rows.New(
            0b01000_00000,
            0b11000_00000,
            0b10000_00000);
    }
}
