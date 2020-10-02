using System;
using System.Diagnostics;

namespace Tetris
{
    public readonly struct Clearing : IEquatable<Clearing>
    {
        private const byte Flag_PerfectClear = 0b0100_0000;
        private const byte Flag_TSpin = 0b0100_0000;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly byte bits;

        public Clearing(int cleared, bool perfect, bool t_spin)
        { 
            bits = (byte)cleared;
            bits |= perfect ? Flag_PerfectClear : default;
            bits |= t_spin ? Flag_TSpin : default;
        }

        public int Rows => bits & 0b0000_0111;
        public bool PerfectClear => (bits & Flag_PerfectClear) != 0;
        public bool TSpin => (bits & Flag_TSpin) != 0;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is Clearing other && Equals(other);

        /// <inheritdoc />
        public bool Equals(Clearing other) => bits == other.bits;

        /// <inheritdoc />
        public override int GetHashCode() => bits;

        /// <inheritdoc />
        public override string ToString() => $"Rows: {Rows}{(TSpin ? ", T-Spin" : "")}";
    }
}
