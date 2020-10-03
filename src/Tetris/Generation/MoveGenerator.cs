using SmartAss;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tetris.Collections;

namespace Tetris.Generation
{
    public class MoveGenerator : IEnumerator<MoveCandidate>, IEnumerable<MoveCandidate>
    {
        private Field field;
        private readonly Visited visted = new Visited();
        private readonly Visited moved = new Visited();
        private readonly FixedQueue<MoveCandidate> queue = new FixedQueue<MoveCandidate>(4000 / 7);

        public MoveGenerator(Field field, Block block)
        {
            this.field = field;

            var candidate = new MoveCandidate(block);

            while (candidate.Offset > field.Filled + 2)
            {
                candidate = candidate.Down();
            }
            queue.Enqueue(candidate);
        }
        public MoveCandidate Current { get; private set; }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (queue.IsEmpty)
            {
                return false;
            }

            var candidate = queue.Dequeue();
            var fit = field.Fits(candidate.Block);

            if (fit == Fit.False)
            {
                return MoveNext();
            }
            else if (fit == Fit.True)
            {
                Enqueue(candidate, candidate.Others);
                return NewMove(candidate) || MoveNext();
            }
            else /* if (fit == Fit.Maybe) */
            {
                Enqueue(candidate, candidate.Nexts);
                return MoveNext();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Enqueue(MoveCandidate candidate, IEnumerable<MoveCandidate> nexts)
        {
            foreach (var next in nexts)
            {
                if (visted.Add(next.Id))
                {
                    queue.Enqueue(candidate.Next(next));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool NewMove(MoveCandidate candidate)
        {
            Current = candidate;
            return moved.Add(candidate.Primary);
        }

        public void Reset() => field = default;

        public IEnumerator<MoveCandidate> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Dispose() => Do.Nothing();
    }
}
