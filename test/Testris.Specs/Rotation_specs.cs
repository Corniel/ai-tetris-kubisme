using NUnit.Framework;
using Tetris;

namespace Rotation_specs
{
    public class Primary
    {
        [TestCase(Rotation.None, Rotation.None)]
        [TestCase(Rotation.Right, Rotation.Right)]
        [TestCase(Rotation.None, Rotation.Uturn)]
        [TestCase(Rotation.Right, Rotation.Left)]
        public void Is(Rotation primary, Rotation rotation)
        {
            Assert.AreEqual(primary, rotation.Primary());
        }
    }

    public class Rotate
    {
        [TestCase(Rotation.Left, Rotation.None)]
        [TestCase(Rotation.None, Rotation.Right)]
        [TestCase(Rotation.Right, Rotation.Uturn)]
        [TestCase(Rotation.Uturn, Rotation.Left)]
        public void Clockwise(Rotation rotated, Rotation rotation)
        {
            Assert.AreEqual(rotated, rotation.Rotate(-1));
        }

        [TestCase(Rotation.Right, Rotation.None)]
        [TestCase(Rotation.Uturn, Rotation.Right)]
        [TestCase(Rotation.Left, Rotation.Uturn)]
        [TestCase(Rotation.None, Rotation.Left)]
        public void CounterClockwise(Rotation rotated, Rotation rotation)
        {
            Assert.AreEqual(rotated, rotation.Rotate(+1));
        }
    }
}
