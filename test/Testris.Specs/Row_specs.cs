using NUnit.Framework;
using Tetris;

namespace Row_specs
{
    public class From_Bits
    {
        [TestCase(0b_00000_00000, "..........")]
        [TestCase(0b_11001_01101, "XX..X.XX.X")]
        [TestCase(0b_11101_11011, "XXX.XXX.XX")]
        [TestCase(0b_11111_11111, "XXXXXXXXXX")]
        [TestCase(0b_00000_00110, ".......XX.")]
        public void Represent_a_row(int bits, string str)
        {
            var row = Row.New(bits);
            Assert.AreEqual(str, row.ToString());
        }

        [TestCase(0b_00000_00000, 0)]
        [TestCase(0b_11001_01101, 6)]
        [TestCase(0b_11101_11011, 8)]
        [TestCase(0b_11111_11111, 10)]
        [TestCase(0b_00000_00110, 2)]
        public void Knows_total_of_bits(int bits, int count)
        {
            var row = Row.New(bits);
            Assert.AreEqual(count, row.Count);
        }
    }

    public class Transform
    {
        [Test]
        public void To_left_shift_bits_to_left()
        {
            var row = Row.New(0b_01000_00110);
            var mov = Row.New(0b_10000_01100);

            Assert.AreEqual(mov, row.Left());
        }


        [Test]
        public void To_right_shift_bits_to_right()
        {
            var row = Row.New(0b_01000_00100);
            var mov = Row.New(0b_00010_00001);

            Assert.AreEqual(mov, row.Right(2));
        }
    }

}