﻿using SmartAss;
using System.Collections.Generic;

namespace Tetris.Generation
{
    public readonly struct MoveCandidate
    {
        public readonly Block Block;
        public readonly Steps Steps;

        public MoveCandidate(Block block) 
            : this(block, Steps.None) => Do.Nothing();

        public MoveCandidate(Block block, Step step)
            : this(block, Steps.None.Add(step)) => Do.Nothing();

        private MoveCandidate(Block block, Steps steps)
        {
            Block = block;
            Steps = steps;
        }

        public MoveCandidate Down()
            => new MoveCandidate(Block.Down, Steps.AddDown());

        public MoveCandidate Next(MoveCandidate candidate)
            => new MoveCandidate(candidate.Block, Steps.Add(candidate.Steps.First));

        internal int Offset => Block.Offset;
        internal short Id => Block.Id; 
        internal short Primary => Block.Primary;

        internal IEnumerable<MoveCandidate> Nexts => Block.Nexts;
        internal IEnumerable<MoveCandidate> Others => Block.Others;
    }
}
