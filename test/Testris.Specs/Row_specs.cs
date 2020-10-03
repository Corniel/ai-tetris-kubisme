using NUnit.Framework;
using System;
using System.Linq;
using Testris.Specs;
using Tetris;

namespace Row_specs
{
    public class All
    {
        [TestCase(07, Shape.I, Rotation.None)]
        [TestCase(10, Shape.I, Rotation.Right)]
        [TestCase(07, Shape.I, Rotation.Uturn)]
        [TestCase(10, Shape.I, Rotation.Left)]
        [TestCase(08, Shape.J, Rotation.None)]
        [TestCase(09, Shape.J, Rotation.Right)]
        [TestCase(08, Shape.J, Rotation.Uturn)]
        [TestCase(09, Shape.J, Rotation.Left)]
        [TestCase(08, Shape.L, Rotation.None)]
        [TestCase(09, Shape.L, Rotation.Right)]
        [TestCase(08, Shape.L, Rotation.Uturn)]
        [TestCase(09, Shape.L, Rotation.Left)]
        [TestCase(09, Shape.O, Rotation.None)]
        [TestCase(08, Shape.S, Rotation.None)]
        [TestCase(09, Shape.S, Rotation.Right)]
        [TestCase(08, Shape.S, Rotation.Uturn)]
        [TestCase(09, Shape.S, Rotation.Left)]
        [TestCase(08, Shape.T, Rotation.None)]
        [TestCase(09, Shape.T, Rotation.Right)]
        [TestCase(08, Shape.T, Rotation.Uturn)]
        [TestCase(09, Shape.T, Rotation.Left)]
        [TestCase(08, Shape.Z, Rotation.None)]
        [TestCase(09, Shape.Z, Rotation.Right)]
        [TestCase(08, Shape.Z, Rotation.Uturn)]
        [TestCase(09, Shape.Z, Rotation.Left)]
        public void Columns_have_values_between_7_and_10(int columns, Shape shape, Rotation rotation)
        {
            Assert.AreEqual(columns, Rows.All().Columns(shape, rotation));
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
