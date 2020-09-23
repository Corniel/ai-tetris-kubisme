using System;

namespace Tetris
{
    public partial class Block
    {
        /// <summary>Gets the next block based on the <see cref="Step"/>.</summary>
        public Block Next(Step step)
            => step switch
            {
                Step.Down => Down,
                Step.Left => Left,
                Step.Right => Right,
                Step.TurnLeft => TurnLeft,
                Step.TurnRight => TurnRight,
                _ => throw new NotSupportedException(),
            };

        /// <summary>Gets the block after a <see cref="Step.Down"/>.</summary>
        public Block Down { get; internal set; }

        /// <summary>Gets the block after a <see cref="Step.Left"/>.</summary>
        public Block Left { get; internal set; }

        /// <summary>Gets the block after a <see cref="Step.Right"/>.</summary>
        public Block Right { get; internal set; }

        /// <summary>Gets the block after a <see cref="Step.TurnLeft"/>.</summary>
        public Block TurnLeft { get; internal set; }

        /// <summary>Gets the block after a <see cref="Step.TurnRight"/>.</summary>
        public Block TurnRight { get; internal set; }
    }
}
