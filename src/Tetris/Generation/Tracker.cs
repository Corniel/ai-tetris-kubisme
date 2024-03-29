﻿using SmartAss.Diagnostics;

namespace Tetris.Generation;

[DebuggerDisplay("{DebuggerDisplay}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public sealed class Tracker : IEnumerable<int>
{
    private const byte Visited = 1;
    private const byte Moved = 255;
    private const int capacity = 600;

    private readonly byte[] lookup = new byte[capacity];

    public int Count => this.Count();

    public bool Visit(int id)
    {
        var value = lookup[id];
        if(value != default)
        {
            return false;
        }
        else
        {
            lookup[id] = Visited;
            return true;
        }
    }

    public bool Move(int id)
    {
        var value = lookup[id];
        if (value == Moved)
        {
            return false;
        }
        else
        {
            lookup[id] = Moved;
            return true;
        }
    }

    public Tracker Clear()
    {
        Array.Clear(lookup, 0, capacity);
        return this;
    }

    public IEnumerator<int> GetEnumerator()
        => lookup
        .Select((hash, i) => i)
        .Where(i => lookup[i] != default)
        .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Represents the map as a DEBUG <see cref="string"/>.</summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"Count: {Count}";
}
