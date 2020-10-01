using SmartAss.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tetris.Generation
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    public sealed class Visited : IEnumerable<int>
    {
        private const byte True = 255;
        private const int capacity = 600;

        private readonly byte[] lookup = new byte[capacity];

        public int Count => this.Count();

        public bool Add(int hash)
        {
            var visited = lookup[hash];
            if(visited == True)
            {
                return false;
            }
            else
            {
                lookup[hash] = True;
                return true;
            }
        }

        public void Clear() => Array.Clear(lookup, 0, capacity);

        public IEnumerator<int> GetEnumerator()
            => lookup
            .Select((hash, i) => i)
            .Where(i => lookup[i] == True)
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Represents the map as a DEBUG <see cref="string"/>.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay => $"Count: {Count}";
    }
}
