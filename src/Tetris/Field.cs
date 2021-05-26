﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Tetris
{
    /// <summary>Represents Testis field.</summary>
    public readonly struct Field : IEquatable<Field>
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
            if (OffField(block) || AboveFilled(block)) return Fit.Maybe;

            var hasFloor = block.HasFloor;

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

        private bool OffField(Block block) => block.Height > Height;
        private bool AboveFilled(Block block) => block.Offset > Filled;

        public Move Move(Block block, Path path)
        {
            foreach (var step in path)
            {
                if (Fits(block) == Fit.False) throw InvalidPath.NoFit();
                else block = block.Next(step); 
            }

            return Fits(block) switch
            {
                Fit.True => Move(block),
                Fit.Maybe => throw InvalidPath.StepsMissing(),
                _ => throw InvalidPath.NoFit(),
            };
        }

        /// <summary>Applies the move described by a <see cref="Mask"/>.</summary>
        public Move Move(Block block)
        {
            var source = block.Offset;
            var target = source;
            var moved = new Row[Math.Max(Filled, block.Height)];

            // copy untouched rows bellow block
            Array.Copy(rows, moved, source);

            // merge touched rows
            while (source < block.Height)
            {
                var merged = this[source].Merge(block[source]);

                if (!merged.IsFull())
                {
                    moved[target++] = merged;
                }

                source++;
            }

            // copy untouched rows above block
            while (source < Filled)
            {
                moved[target++] = rows[source++];
            }

            var clearing = new Clearing(
                cleared: source - target,
                perfect: moved[0].IsEmpty(),
                t_spin: false); // TODO

            var field = new Field(
                rows: moved,
                height: height,
                filled: (byte)(target));

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

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is Field other && Equals(other);

        /// <inheritdoc />
        public bool Equals(Field other)
        {
            if (Filled != other.Filled || Height != other.Height) { return false; }
            for (var r = 0; r < Filled; r++)
            {
                if (rows[r] != other.rows[r]) { return false; }
            }
            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hash = 0;

            for (var r = 0; r < Filled; r++)
            {
                hash *= 17;
                hash ^= rows[r].GetHashCode();
            }
            return hash;
        }

        public static Field New(params ushort[] rows)
        {
            var h = rows.Length;
            var rs = Rows.Trim(rows);
            return new Field(rs, (byte)h, (byte)rs.Length);
        }

        public static Field Parse(string str, string pattern = ".X")
            => New(str.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(line => Convert.ToUInt16(line.Replace(pattern[0], '0').Replace(pattern[1], '1'), 2))
            .ToArray());
    }
}
