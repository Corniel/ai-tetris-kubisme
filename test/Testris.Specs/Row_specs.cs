using NUnit.Framework;
using System;
using System.Linq;
using Testris.Specs;
using Tetris;

namespace Row_specs
{
    public class All
    {
        private const int None = 0;
        private const int Right = 1;
        private const int UTurn = 2;
        private const int Left = 3;

        [TestCase(07, Shape.I, None)]
        [TestCase(10, Shape.I, Right)]
        [TestCase(07, Shape.I, UTurn)]
        [TestCase(10, Shape.I, Left)]
        [TestCase(08, Shape.J, None)]
        [TestCase(09, Shape.J, Right)]
        [TestCase(08, Shape.J, UTurn)]
        [TestCase(09, Shape.J, Left)]
        [TestCase(08, Shape.L, None)]
        [TestCase(09, Shape.L, Right)]
        [TestCase(08, Shape.L, UTurn)]
        [TestCase(09, Shape.L, Left)]
        [TestCase(09, Shape.O, None)]
        [TestCase(08, Shape.S, None)]
        [TestCase(09, Shape.S, Right)]
        [TestCase(08, Shape.S, UTurn)]
        [TestCase(09, Shape.S, Left)]
        [TestCase(08, Shape.T, None)]
        [TestCase(09, Shape.T, Right)]
        [TestCase(08, Shape.T, UTurn)]
        [TestCase(09, Shape.T, Left)]
        [TestCase(08, Shape.Z, None)]
        [TestCase(09, Shape.Z, Right)]
        [TestCase(08, Shape.Z, UTurn)]
        [TestCase(09, Shape.Z, Left)]
        public void Columns_have_values_between_7_and_10(int columns, Shape shape, int rotation)
        {
            Assert.AreEqual(columns, Rows.All().Columns(shape, new Rotation(rotation)));
        }

        [Test]
        public void Are_initialized_within_5ms()
        {
            Speed.Runs(() => Rows.All(), 1, TimeSpan.FromMilliseconds(5));
        }

        [Test]
        public void Contains_213_rows()
        {
            Assert.AreEqual(213, Rows.All().ToArray().Length);
        }

        [Test]
        public void Rows_have_4_bit()
        {
            Assert.AreEqual(0, Rows.All().Count(rows => rows.Count() != 4));
        }
    }

    public class From_Bits
    {
        [TestCase(0b_00000_00000, "..........")]
        [TestCase(0b_11001_01101, "XX..X.XX.X")]
        [TestCase(0b_11101_11011, "XXX.XXX.XX")]
        [TestCase(0b_11111_11111, "XXXXXXXXXX")]
        [TestCase(0b_00000_00110, ".......XX.")]
        public void Represent_a_row(int bits, string str)
        {
            var row = Row.New((ushort)bits);
            Assert.AreEqual(str, row.ToString());
        }

        [TestCase(0b_00000_00000, 0)]
        [TestCase(0b_11001_01101, 6)]
        [TestCase(0b_11101_11011, 8)]
        [TestCase(0b_11111_11111, 10)]
        [TestCase(0b_00000_00110, 2)]
        public void Knows_total_of_bits(int bits, int count)
        {
            var row = Row.New((ushort)bits);
            Assert.AreEqual(count, row.Count);
        }
    }

    public class Transform
    {
        [Test]
        public void To_right_shift_bits_to_right()
        {
            var row = Row.New(0b_01000_00100);
            var mov = Row.New(0b_00100_00010);

            Assert.AreEqual(mov, row.Right());
        }
    }

}
