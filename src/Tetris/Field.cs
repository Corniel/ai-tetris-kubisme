﻿using System;
using System.Diagnostics;
using System.Text;

namespace Tetris
{
    /// <summary>Represents Testis field.</summary>
    public readonly struct Field
    {
        public static readonly Field Start = new Field(Array.Empty<Row>(), 20, 0);

        private Field(Row[] rows, byte height, byte filled)
        {
            this.rows = rows;
            this.height = height;
            Filled = filled;
        }

        private readonly Row[] rows;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly byte height;

        /// <summary>Gets total of filled rows.</summary>
        public readonly byte Filled;
        
        /// <summary>Gets the height of the field.</summary>
        public int Height => height;

        /// <summary>Gets the number of free rows.</summary>
        public int Free => Height - Filled;

        /// <summary>Gets a <see cref="Row"/> based on a (zero based) row number.</summary>
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

            // Nothing to match yet
            if (block.Offset > Filled) return Fit.Maybe;

            // the bottom of the field is a solid floor too.
            var hasFloor = block.Offset == 0;

            var row = block.Offset;
            var end = block.Height;

            while (row < end)
            {
                if (block[row].HasOverlapWith(this[row]))
                {
                    return Fit.False;
                }
                else if (!hasFloor)
                {
                    hasFloor = block[row].HasOverlapWith(this[row - 1]);
                }
                row++;
            }

            return hasFloor ? Fit.True : Fit.Maybe;
        }

        public Move Move(Block block, Steps steps)
        {
            var fit = Fit.Maybe;

            foreach (var step in steps)
            {
                // ignore further steps.
                if (block is null) { break; }

                fit = Fits(block);

                if (fit == Fit.False) { return default; }
                else if (fit == Fit.True) { return Move(block); }
                else { block = block.Next(step); }
            }
            while (fit == Fit.Maybe)
            {
                fit = Fits(block);
                block = block.Down;
            }
            return Move(block);
        }

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

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var h = height - 1; h >= 0; h--)
            {
                if (h > Filled)
                {
                    sb.AppendLine(Row.Empty.ToString());
                }
                else
                {
                    sb.AppendLine(this[h].ToString());
                }
            }
            return sb.ToString();
        }

        public static Field New(params ushort[] rows)
        {
            var h = rows.Length;
            var rs = Rows.Trim(rows);
            return new Field(rs, (byte)h, (byte)rs.Length);
        }
    }
}
