using SmartAss;
using System.Collections;
using System.Collections.Generic;

namespace Tetris.Kubisme.Generation
{
    public class MoveGenerator : IEnumerator<Move>, IEnumerable<Move>
    {
        private const byte True = 255;
        private Field field;
        private readonly byte[] done = new byte[4000];
        private readonly Queue<Block> queue = new Queue<Block>();

        public MoveGenerator(Field field, Block block)
        {
            this.field = field;
            queue.Enqueue(block);
        }
        public Move Current { get; private set; }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (queue.Count == 0) { return false; }

            var block = queue.Dequeue();
            var fit = field.Fits(block);

            if (fit == Fit.False)
            {
                return MoveNext();
            }
            else
            {
                Enqueue(block);
                return fit == Fit.True
                    ? Move(block)
                    : MoveNext();
            }
        }

        private void Enqueue(Block block)
        {
            if (block.Offset == 0 && block.Column < 2)
            {

            }

            foreach (var next in block.Explore)
            {
                if (done[next.Id] == 0)
                {
                    done[next.Id] = True;
                    queue.Enqueue(next);
                }
            }
        }

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
