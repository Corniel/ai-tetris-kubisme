using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    public readonly struct Field : Rows
    {
        internal Field(Row[] rows, byte height, byte filled)
        {
            this.rows = rows;
            this.height = height;
            Filled = filled;
        }

        private readonly byte height;
        public readonly byte Filled;
        private readonly Row[] rows;

        public int Height => height;
        public Row this[int row] => row >= Filled ? Row.Empty : rows[row];

        /// <summary>Investigates if the block fit.</summary>
        /// <returns>
        /// Returns false if there is overlap.
        /// Else, returns true if solid ground, or retry if not.
        /// </returns>
        public Fit Fits(Block block)
        {
            // Not enough space.
            if (block.Height > Height) return Fit.False;

            // the bottom of the field is a solid floor too.
            var hasFloor = block.Offset == 0;

            var row = block.Offset;
            var end = block.Height;

            while (row < end)
            {
                if (block[row].HaveOverlap(this[row]))
                {
                    return Fit.False;
                }
                else if (!hasFloor)
                {
                    hasFloor = block[row].HaveOverlap(this[row - 1]);
                }
                row++;
            }

            return hasFloor ? Fit.True : Fit.Maybe;
        }

        public int Free => Height - Filled;

        /// <summary>Applies the move described by a <see cref="Mask"/>.</summary>
        public Move Move(Block block)
        {
            var source = block.Offset;
            var target = source;
            var moved = new Row[Math.Max(Filled, block.Height)];

            // copy untouched rows
            Array.Copy(rows, moved, source);

            // merge touched rows
            while (source < moved.Length)
            {
                moved[target] = this[source].Merge(block[source]);
                source++;
                target += moved[target].IsFull() ? 1 : 0;
            }

            var clearing = new Clearing(
                cleared: source - target,
                perfect: moved[0].IsEmpty(),
                t_spin: false); // TODO

            var field = new Field(
                rows: moved,
                height: height,
                filled: (byte)(target + 1));

            return new Move(clearing, field);
        }
    }
}
