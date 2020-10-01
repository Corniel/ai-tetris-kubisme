using SmartAss;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tetris.Collections;

namespace Tetris.Generation
{
    public class MoveGenerator : IEnumerator<MoveCandidate>, IEnumerable<MoveCandidate>
    {
        private const byte True = 255;
        private Field field;
        private readonly byte[] done = new byte[4000];
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
            if (queue.IsEmpty) { return false; }

            var candidate = queue.Dequeue();
            var fit = field.Fits(candidate.Block);

            if (fit == Fit.False)
            {
                return MoveNext();
            }
            else if (fit == Fit.True)
            {
                Enqueue(candidate, candidate.Block.Others);
                return Set(candidate);
            }
            else /* if (fit == Fit.Maybe) */
            {
                Enqueue(candidate, candidate.Block.Nexts);
                return MoveNext();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Enqueue(MoveCandidate candidate, IEnumerable<MoveCandidate> nexts)
        {
            foreach (var next in nexts)
            {
                if (done[next.Id] == 0)
                {
                    done[next.Id] = True;
                    queue.Enqueue(candidate.Next(next));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool Set(MoveCandidate candidate)
        {
            // TODO: for blocks that have a rotated
            // version, check if that one has been passed.
            Current = candidate;
            return true;
        }

        public void Reset() => field = default;

        public IEnumerator<MoveCandidate> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Dispose() => Do.Nothing();
    }
}
