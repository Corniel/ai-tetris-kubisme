using System.Runtime.CompilerServices;
using Tetris.Collections;
using Tetris.ObjectPooling;

namespace Tetris.Generation;

public sealed class MoveGenerator : IEnumerator<MoveCandidate>, IEnumerable<MoveCandidate>
{
    private static readonly ObjectPool<FixedQueue<MoveCandidate>> Queues = new(() => new(4000 / 7));
    private static readonly ObjectPool<Tracker> Trackers = new(() => new());

    private readonly Field field;
    private readonly Tracker tracker;
    private readonly FixedQueue<MoveCandidate> queue;

    public MoveGenerator(Field field, Block block)
    {
        this.field = field;
        tracker = Trackers.Get().Clear();
        queue = Queues.Get().Clear();

        var candidate = new MoveCandidate(block);

        while (candidate.Offset > field.Filled + 2)
        {
            candidate = candidate.Down();
        }
        queue.Enqueue(candidate);
    }
    
    public MoveCandidate Current { get; private set; }

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (queue.IsEmpty) return false;

        var candidate = queue.Dequeue();
        var fit = field.Fits(candidate.Block);

        if (fit == Fit.Maybe)
        {
            Enqueue(candidate, candidate.Nexts);
            return MoveNext();
        }
        else if (fit == Fit.False) return MoveNext();
        else /* fit == Fit.True */
        {
            Enqueue(candidate, candidate.Others);
            return NewMove(candidate) || MoveNext();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Enqueue(MoveCandidate candidate, IEnumerable<MoveCandidate> nexts)
    {
        foreach (var next in nexts.Where(n => tracker.Visit(n.Id)))
        {
            queue.Enqueue(candidate.Next(next));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool NewMove(MoveCandidate candidate)
    {
        Current = candidate;
        return tracker.Move(candidate.Primary);
    }

    public IEnumerator<MoveCandidate> GetEnumerator() => this;
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Dispose() 
    {
        Trackers.Add(tracker);
        Queues.Add(queue);
    }

    public void Reset() => throw new NotSupportedException();
}
