using SmartAss.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.FormattableString;

namespace Tetris.Collections
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    public class FixedQueue<T> : IEnumerable<T>
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

        public void Prioritize(T item) => queue[--tail] = item;

        public void Enqueue(T item) => queue[head++] = item;

        public T Dequeue() => queue[tail++];

        public void Clear()
        {
            head = 0;
            tail = 0;
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => queue
            .Skip(tail)
            .Take(Count)
            .GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Represents the map as a DEBUG <see cref="string"/>.</summary>
        protected virtual string DebuggerDisplay => Invariant($"Count: {Count:#,##0}");
    }
}
