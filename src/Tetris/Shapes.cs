namespace Tetris
{
    public static class Shapes
    {
        public static readonly Row[] I = Rows.New(
            0b1111);

        public static readonly Row[] I_R = Rows.New(
            0b1,
            0b1,
            0b1,
            0b1);

        public static readonly Row[] I_U = Rows.New(
           0b1111);

        public static readonly Row[] I_L = Rows.New(
            0b1,
            0b1,
            0b1,
            0b1);

        public static readonly Row[] J = Rows.New(
           0b100,
           0b111);

        public static readonly Row[] J_R = Rows.New(
            0b11,
            0b10,
            0b10);

        public static readonly Row[] J_U = Rows.New(
            0b111,
            0b001);

        public static readonly Row[] J_L = Rows.New(
            0b01,
            0b01,
            0b11);

        public static readonly Row[] L = Rows.New(
            0b001,
            0b111);

        public static readonly Row[] L_R = Rows.New(
            0b10,
            0b10,
            0b11);

        public static readonly Row[] L_U = Rows.New(
            0b111,
            0b100);

        public static readonly Row[] L_L = Rows.New(
            0b11,
            0b01,
            0b01);

        public static readonly Row[] O = Rows.New(
           0b11,
           0b11);

        public static readonly Row[] S = Rows.New(
           0b011,
           0b110);

        public static readonly Row[] S_R = Rows.New(
            0b10,
            0b11,
            0b01);

        public static readonly Row[] S_U = Rows.New(
            0b011,
            0b110);

        public static readonly Row[] S_L = Rows.New(
            0b10,
            0b11,
            0b01);

        public static readonly Row[] T = Rows.New(
            0b010,
            0b111);

        public static readonly Row[] T_R = Rows.New(
            0b10,
            0b11,
            0b10);

        public static readonly Row[] T_U = Rows.New(
            0b111,
            0b010);

        public static readonly Row[] T_L = Rows.New(
            0b01,
            0b11,
            0b01);

        public static readonly Row[] Z = Rows.New(
            0b110,
            0b011);

        public static readonly Row[] Z_R = Rows.New(
            0b01,
            0b11,
            0b10);

        public static readonly Row[] Z_U = Rows.New(
            0b110,
            0b011);

        public static readonly Row[] Z_L = Rows.New(
            0b01,
            0b11,
            0b10);
    }
}
