using SmartAss;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tetris.Collections;

namespace Tetris.Generation
{
    public partial class MoveGenerator : IEnumerator<MoveCandidate>, IEnumerable<MoveCandidate>
    {
        private Field field;

        private readonly Tracker tracker = new Tracker();
        private readonly FixedQueue<MoveCandidate> queue = new FixedQueue<MoveCandidate>(600);

        private MoveGenerator(Field field) => this.field = field;

        public MoveCandidate Current { get; private set; }

        object IEnumerator.Current => Current;

        /// <inheritdoc />
        public bool MoveNext()
        {
            if (queue.IsEmpty)
            {
                return false;
            }

            Current = queue.Dequeue();
            var fit = field.Fits(Current.Block);

            if (fit == Fit.False)
            {
                return MoveNext();
            }
            else if (fit == Fit.True)
            {
                Enqueue(Current.Others);
                return IsNewMove() || MoveNext();
            }
            else /* if (fit == Fit.Maybe) */
            {
                Enqueue(Current.Nexts);
                return MoveNext();
            }
        }

        /// <summary>Adds new move candidates to the queue.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Enqueue(IEnumerable<MoveCandidate> candidates)
        {
            foreach (var next in candidates)
            {
                if (tracker.Visit(next.Id))
                {
                    queue.Enqueue(Current.Next(next));
                }
            }
        }

        /// <summary>Returns true if the current is a new move.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsNewMove() => tracker.Move(Current.Primary);

        /// <inheritdoc />
        public IEnumerator<MoveCandidate> GetEnumerator() => this;

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public void Reset()
        {
            queue.Clear();
            tracker.Clear();
        }

        /// <inheritdoc />
        public void Dispose() => Do.Nothing();
    }
}
