using System;
using System.Diagnostics.CodeAnalysis;

namespace Tetris.MonteCarlo
{
    public readonly struct Expected : IComparable<Expected>
    {
        private readonly int count;
        private readonly long total;

        private Expected(int cnt, long scr)
        {
            count = cnt;
            total = scr;
        }

        public double Value => count == 0 ? 0 : total / ((double)count);

        public Expected Add(int score) => new Expected(count + 1, score + total);

        public int CompareTo(Expected other) => Value.CompareTo(other.Value);

        public override string ToString() => $"{Value:#,##0.0}, Count: {count}";

        public static Expected operator +(Expected score, int value) => score.Add(value);
    }
}
