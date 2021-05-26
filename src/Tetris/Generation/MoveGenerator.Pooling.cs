using SmartAss.Pooling;

namespace Tetris.Generation
{
    public partial class MoveGenerator
    {
        private static readonly ObjectPool<MoveGenerator> pool = new ObjectPool<MoveGenerator>(256)
            .Populate(() => new MoveGenerator(), 64);

        public static MoveGenerator New(Field field, Block block)
            => pool.Get(() => new MoveGenerator()).Init(field, block);

        private MoveGenerator Init(Field field, Block block)
        {
            Reset();
            this.field = field;
            Current = new MoveCandidate(block);
            tracker.Visit(Current.Id);
            queue.Enqueue(Current);

            return this;
        }

        public void Release() => pool.Release(this);
    }
}
