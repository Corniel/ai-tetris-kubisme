using SmartAss;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tetris.Collections;

namespace Tetris.Generation
{
    public class MoveGenerator : IEnumerator<Move>, IEnumerable<Move>
    {
        private const byte True = 255;
        private Field field;
        private readonly byte[] done = new byte[4000];
        private readonly FixedQueue<Block> queue = new FixedQueue<Block>(4000 / 7);

        public MoveGenerator(Field field, Block block)
        {
            this.field = field;
            while (block.Offset > field.Filled + 2)
            {
                block = block.Down;
            }
            queue.Enqueue(block);
        }
        public Move Current { get; private set; }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (queue.IsEmpty) { return false; }

            var block = queue.Dequeue();
            var fit = field.Fits(block);

            if (fit == Fit.False)
            {
                return MoveNext();
            }
            else if (fit == Fit.True)
            {
                Enqueue(block.Others);
                return Move(block);
            }
            else /* if (fit == Fit.Maybe) */
            {
                Enqueue(block.Nexts);
                return MoveNext();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Enqueue(IEnumerable<Block> blocks)
        {
            foreach (var next in blocks)
            {
                if (done[next.Id] == 0)
                {
                    done[next.Id] = True;
                    queue.Enqueue(next);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool Move(Block block)
        {
            // TODO: for blocks that have a rotated
            // version, check if that one has been passed.
            Current = field.Move(block);
            return true;
        }

        public void Reset() => field = default;

        public IEnumerator<Move> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Dispose() => Do.Nothing();
    }
}
