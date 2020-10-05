using NUnit.Framework;
using System.Linq;
using Tetris;

namespace Steps_specs
{
    public class Create
    {
        [Test]
        public void From_array_of_5_contains_full_input()
        {
            var array = new [] { Step.Left, Step.Right, Step.Down, Step.TurnLeft, Step.TurnRight };
            var path = Path.Create(array);

            Assert.AreEqual(5, path.Length);
            Assert.AreEqual(array, path.ToArray());
            Assert.AreEqual("left,right,down,turnleft,turnright", path.ToString());
        }

        [Test]
        public void From_array_of_25_contains_full_input()
        {
            var array = new[]
            {
                Step.Left, Step.Right, Step.Down, Step.TurnLeft, Step.TurnRight,
                Step.Left, Step.Right, Step.Down, Step.TurnLeft, Step.TurnRight,
                Step.Left, Step.Right, Step.Down, Step.TurnLeft, Step.TurnRight,
                Step.Left, Step.Right, Step.Down, Step.TurnLeft, Step.TurnRight,
                Step.Left, Step.Right, Step.Down, Step.TurnRight, Step.TurnRight 
            };

            var steps = Path.Create(array);

            Assert.AreEqual(25, steps.Length);
            Assert.AreEqual(array, steps.ToArray());
            Assert.AreEqual(
                "left,right,down,turnleft,turnright," +
                "left,right,down,turnleft,turnright," +
                "left,right,down,turnleft,turnright," +
                "left,right,down,turnleft,turnright," +
                "left,right,down,turnright,turnright",
                steps.ToString());
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void Down(int st)
        {
            var steps = Path.Down(st);

            Assert.AreEqual(st, steps.Length);
            Assert.AreEqual(st, steps.Count());
            Assert.IsTrue(steps.All(step => step == Step.Down));
        }
    }
}
