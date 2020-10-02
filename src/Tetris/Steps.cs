using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tetris
{
	public readonly struct Steps :
		IEquatable<Steps>,
		IEnumerable<Step>
	{
		private const ulong Mask = 0x0FFF_FFFF_FFFF_FFFF;

		public static readonly Steps None;

		private Steps(ulong m0, ulong m1, int count)
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

		public int Count
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
				var shft = Count * 3;
				return shft < 60
					? (Step)((steps0 >> shft) & 7)
					: (Step)((steps1 >> (shft - 60)) & 7);
			}
		}

		public Steps Add(Step step)
		{
			var count = Count;
			var m0 = steps0 & Mask;
			var m1 = steps1 & Mask;

			if (count < 20)
			{
				m0 |= ((ulong)step) << (3 * count);
			}
			else
			{
				m1 |= ((ulong)step) << (3 * (count - 20));
			}
			return new Steps(m0, m1, count + 1);
		}

		public Steps AddTurnLeft() => Add(Step.TurnLeft);
		public Steps AddTurnRight() => Add(Step.TurnRight);
		public Steps AddLeft() => Add(Step.Left);
		public Steps AddRight() => Add(Step.Right);
		public Steps AddDown() => Add(Step.Down);
		public Steps AddDrop() => Add(Step.Drop);

		/// <inheritdoc />
		public override int GetHashCode()
			=> steps0.GetHashCode() ^ steps1.GetHashCode();

		/// <inheritdoc />
		public override bool Equals(object obj)
			=> obj is Steps other && Equals(other);

		/// <inheritdoc />
		public bool Equals(Steps other)
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
			var count = Count * 3;
			var c1 = count > 60 ? 60 : count;
			for (var i = 0; i < c1; i += 3)
			{
				var step = (Step)((steps0 >> i) & 7);
				yield return step;
			}
			count -= 60;
			for (var i = 0; i < count; i += 3)
			{
				var step = (Step)((steps1 >> i) & 7);
				yield return step;
			}
		}

		public static Steps Create(params Step[] steps)
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
			return new Steps(m0, m1, p / 3);
		}

		public static Steps Down(int steps) => downs[steps];

		private static readonly Steps[] downs = new []
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
