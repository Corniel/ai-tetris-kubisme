using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tetris
{
	public readonly struct Path :
		IEquatable<Path>,
		IEnumerable<Step>
	{
		private const ulong Mask = 0x0FFF_FFFF_FFFF_FFFF;

		public static readonly Path None;

		private Path(ulong m0, ulong m1, int count)
		{
			m0 |= ((ulong)count) << 60;
			m1 |= ((ulong)count >> 4) << 60;
			steps0 = m0;
			steps1 = m1;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ulong steps0;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ulong steps1;

        /// <summary>Gets the length of the path.</summary>
		public int Length
		{
			get
			{
				int c = (int)(steps0 >> 60);
				c |= ((int)(steps1 >> 60)) << 4;
				return c;
			}
		}

        /// <summary>Gets the first added action.</summary>
        public Step First => (Step)(steps0 & 7);

        /// <summary>Gets the last added action.</summary>
        /// <remarks>
        /// Useful to prevent infinite left-right switches.
        /// </remarks>
        public Step Last
		{
			get
			{
				var shft = Length * 3;
				return shft < 60
					? (Step)((steps0 >> shft) & 7)
					: (Step)((steps1 >> (shft - 60)) & 7);
			}
		}

        /// <summary>Gets the totals of downs.</summary>
        public int Downs
        {
            get
            {
                var total = 0;

                var length = Length * 3;
                var c1 = length > 60 ? 60 : length;
                for (var i = 0; i < c1; i += 3)
                {
                    var step = (Step)((steps0 >> i) & 7);
                    if (step == Step.Down) { total++; }
                }
                length -= 60;
                for (var i = 0; i < length; i += 3)
                {
                    var step = (Step)((steps1 >> i) & 7);
                    if (step == Step.Down) { total++; }
                }

                return total;
            }
        }

        public Path Add(Step step)
		{
			var length = Length;
			var m0 = steps0 & Mask;
			var m1 = steps1 & Mask;

			if (length < 20)
			{
				m0 |= ((ulong)step) << (3 * length);
			}
			else
			{
				m1 |= ((ulong)step) << (3 * (length - 20));
			}
			return new Path(m0, m1, length + 1);
		}

		public Path AddTurnLeft() => Add(Step.TurnLeft);
		public Path AddTurnRight() => Add(Step.TurnRight);
		public Path AddLeft() => Add(Step.Left);
		public Path AddRight() => Add(Step.Right);
		public Path AddDown() => Add(Step.Down);
		public Path AddDrop() => Add(Step.Drop);

		/// <inheritdoc />
		public override int GetHashCode()
			=> steps0.GetHashCode() ^ steps1.GetHashCode();

		/// <inheritdoc />
		public override bool Equals(object obj)
			=> obj is Path other && Equals(other);

		/// <inheritdoc />
		public bool Equals(Path other)
			=> other.steps0 == steps0
			&& other.steps1 == steps1;

		/// <inheritdoc />
		public override string ToString()
			=> steps0 == 0
			? "no_steps"
			: string.Join(",", this).ToLowerInvariant();

		/// <inheritdoc />
		public IEnumerator<Step> GetEnumerator() => Enumerate().GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private IEnumerable<Step> Enumerate()
		{
			var length = Length * 3;
			var c1 = length > 60 ? 60 : length;
			for (var i = 0; i < c1; i += 3)
			{
				var step = (Step)((steps0 >> i) & 7);
				yield return step;
			}
			length -= 60;
			for (var i = 0; i < length; i += 3)
			{
				var step = (Step)((steps1 >> i) & 7);
				yield return step;
			}
		}

		public static Path Create(params Step[] steps)
		{
			ulong m0 = 0;
			ulong m1 = 0;
			int p = 0;

			foreach (var step in steps)
			{
				if (p < 60)
				{
					m0 |= ((ulong)step) << p;
				}
				else
				{
					m1 |= ((ulong)step) << (p - 60);
				}
				p += 3;
			}
			return new Path(m0, m1, p / 3);
		}

		public static Path Down(int steps) => downs[steps];

		private static readonly Path[] downs = new []
		{
			None,
			Create(Step.Down),
			Create(Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
			Create(Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down, Step.Down),
		};
	}
}
