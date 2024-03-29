﻿namespace Tetris.Collections;

[DebuggerDisplay("{DebuggerDisplay}")]
[DebuggerTypeProxy(typeof(SmartAss.Diagnostics.CollectionDebugView))]
public sealed class FixedQueue<T> : IEnumerable<T>
{
    private readonly T[] queue;
    private int head;
    private int tail;

    public FixedQueue(int capacity)
    {
        queue = new T[capacity];
    }

    public int Count => head - tail;

    public bool IsEmpty => head == tail;

    public bool HasAny => head != tail;

    public void Enqueue(T tile) => queue[head++] = tile;

    public T Dequeue() => queue[tail++];

    public FixedQueue<T> Clear()
    {
        head = 0;
        tail = 0;
        return this;
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator() => queue
        .Skip(tail)
        .Take(Count)
        .GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Represents the map as a DEBUG <see cref="string"/>.</summary>
    private string DebuggerDisplay => Invariant($"Count: {Count:#,##0}");
}
