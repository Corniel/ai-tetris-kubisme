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

        private MoveGenerator() => Do.Nothing();

        public MoveCandidate Current { get; private set; }

        object IEnumerator.Current => Current;

        /// <inheritdoc />
        public bool MoveNext()
        {
            while (queue.HasAny)
            {
                Current = queue.Dequeue();
                var fit = field.Fits(Current.Block);

                while (fit == Fit.Maybe)
                {
                    Enqueue();
                    Current = Current.Down();
                    fit = field.Fits(Current.Block);
                }

                if (fit == Fit.True)
                {
                    Enqueue();
                    if (IsNewMove()) return true;
                }
                else { /* No fit, so no enqueue. */ }
            }
            return false;
        }

        /// <summary>Adds new move candidates to the queue.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Enqueue()
        {
            foreach (var next in Current.Nexts(field.Filled))
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
