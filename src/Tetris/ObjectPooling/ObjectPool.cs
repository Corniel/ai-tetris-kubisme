namespace Tetris.ObjectPooling;

[DebuggerDisplay("Count = {Count}, Capacity = {Pool.Length}")]
public sealed class ObjectPool<T> where T : class
{
    private readonly Func<T> Init;
    private readonly T[] Pool;

    public ObjectPool(Func<T> init, int capacity = 128, int prepare = 8)
    {
        Init = init;
        Pool = new T[capacity];

        for (var i = 0; i < prepare; i++)
        {
            Add(init());
        }
    }

    public int Count { get; private set; }

    public T Get()
    {
        lock (locker)
        {
            return Count == 0 ? Init() : Pool[--Count];
        }
    }

    public void Add(T item)
    {

        lock (locker)
        {
            if (Count < Pool.Length)
            {
                Pool[Count++] = item;

            }
        }
    }

    private readonly object locker = new();
}
