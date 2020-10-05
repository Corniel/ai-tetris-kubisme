using NUnit.Framework;
using Tetris.Generation;

namespace Tracker_specs
{
    public class Visit
    {
        [Test]
        public void Once_returns_true()
        {
            var tracker = new Tracker();
            Assert.IsTrue(tracker.Visit(500));
            Assert.AreEqual(1, tracker.Count);
        }

        [Test]
        public void Moved_already_returns_false()
        {
            var tracker = new Tracker();
            tracker.Move(500);
            Assert.IsFalse(tracker.Visit(500));
            Assert.AreEqual(1, tracker.Count);
        }

        [Test]
        public void Twice_returns_false()
        {
            var tracker = new Tracker();
            tracker.Visit(500);
            Assert.IsFalse(tracker.Visit(500));
            Assert.AreEqual(1, tracker.Count);
        }
    }
    public class Move
    {
        [Test]
        public void Once_returns_true()
        {
            var tracker = new Tracker();
            Assert.IsTrue(tracker.Move(500));
            Assert.AreEqual(1, tracker.Count);
        }

        [Test]
        public void Visit_already_returns_true()
        {
            var tracker = new Tracker();
            tracker.Visit(500);
            Assert.IsTrue(tracker.Move(500));
            Assert.AreEqual(1, tracker.Count);
        }

        [Test]
        public void Twice_returns_false()
        {
            var tracker = new Tracker();
            tracker.Move(500);
            Assert.IsFalse(tracker.Move(500));
            Assert.AreEqual(1, tracker.Count);
        }
    }
    public class Clear
    { 
        [Test]
        public void Resets_visits_and_moves()
        {
            var tracker = new Tracker();
            tracker.Visit(500);
            tracker.Move(300);
            tracker.Clear();
            Assert.AreEqual(0, tracker.Count);
        }
    }
}
