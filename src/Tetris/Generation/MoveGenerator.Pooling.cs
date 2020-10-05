using SmartAss.Pooling;

namespace Tetris.Generation
{
    public partial class MoveGenerator
    {
        private static readonly ObjectPool<MoveGenerator> pool = new ObjectPool<MoveGenerator>(256);

        public static MoveGenerator New(Field field, Block block)
        {
            var generator = pool.Get(()=> new MoveGenerator(field));
            return generator.Init(block);
        }

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
