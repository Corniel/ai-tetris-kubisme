﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tetris.Generation;

namespace Tetris
{
    public partial class Block
    {
        /// <summary>Gets the next block based on the <see cref="Step"/>.</summary>
        public Block Next(Step step)
            => step switch
            {
                Step.None => Down,
                Step.Drop => Down,
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

        /// <summary>Gets the blocks to explore (excluding down) after this one.</summary>
        /// <remarks>
        /// Order:
        /// - <see cref="Left"/>
        /// - <see cref="Right"/>
        /// - <see cref="TurnLeft"/>
        /// - <see cref="TurnRight"/>
        /// </remarks>
        public IEnumerable<MoveCandidate> Nexts(int filled)
            => filled >= EnqueueLimit
            ? EnqueueCandidates
            : Array.Empty<MoveCandidate>();

        internal void InitNexts()
        {
            EnqueueCandidates = new[] 
            {
               new MoveCandidate(Left, Step.Left),
               new MoveCandidate(Right, Step.Right),
               new MoveCandidate(TurnLeft, Step.TurnLeft),
               new MoveCandidate(TurnRight, Step.TurnRight),
            }
            .Where(c => c.Block is Block)
            .ToArray();

            EnqueueLimit = EnqueueCandidates.Min(next => next.Block.Offset);
        }
        private MoveCandidate[] EnqueueCandidates;
        private int EnqueueLimit;
    }
}
