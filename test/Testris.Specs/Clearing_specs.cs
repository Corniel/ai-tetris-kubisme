using NUnit.Framework;
using Tetris;

namespace Clearing_specs
{
    public class Ctor
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Sets_rows_cleared(int rows)
        {
            var clearing = new Clearing(rows, false, false);
            Assert.IsFalse(clearing.TSpin);
            Assert.AreEqual(rows, clearing.Rows);
        }
    }

    public class WithTSpin
    {
        [Test]
        public void Is_toggled()
        {
            var clearing = new Clearing(2, false, true);
            Assert.IsTrue(clearing.TSpin);
            Assert.AreEqual(2, clearing.Rows);
        }
    }

    public class ToString
    {
        [Test]
        public void Shows_rows()
        {
            var clearing = new Clearing(4, false, false);
            Assert.AreEqual("Rows: 4", clearing.ToString());
        }

        [Test]
        public void Shows_TSpin()
        {
            var clearing = new Clearing(1, false, true);
            Assert.AreEqual("Rows: 1, T-Spin", clearing.ToString());
        }
    }

}
