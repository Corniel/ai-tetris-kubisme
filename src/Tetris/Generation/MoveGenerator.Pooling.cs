using SmartAss.Pooling;

namespace Tetris.Generation
{
    public partial class MoveGenerator
    {
        private static readonly ObjectPool<MoveGenerator> pool = new ObjectPool<MoveGenerator>(256)
            .Populate(() => new MoveGenerator(Field.Start), 64);

        public static MoveGenerator New(Field field, Block block)
           => pool.Get(() => new MoveGenerator(field)).Init(block);

        private MoveGenerator Init(Block block)
        {
            Reset();

            Current = new MoveCandidate(block);

            while (Current.Offset > field.Filled + 2)
            {
                Current = Current.Down();
            }
            tracker.Visit(Current.Id);
            queue.Enqueue(Current);

            return this;
        }

        public void Release() => pool.Release(this);
    }
}
