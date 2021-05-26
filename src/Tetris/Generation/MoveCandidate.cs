using SmartAss;
using System.Collections.Generic;

namespace Tetris.Generation
{
    public readonly struct MoveCandidate
    {
        public readonly Block Block;
        public readonly Path Path;

        public MoveCandidate(Block block) 
            : this(block, Path.None) => Do.Nothing();

        internal MoveCandidate(Block block, Step step)
            : this(block, Path.None.Add(step)) => Do.Nothing();

        private MoveCandidate(Block block, Path path)
        {
            Block = block;
            Path = path;
        }

        public MoveCandidate Down()
            => new MoveCandidate(Block.Down, Path.AddDown());

        public MoveCandidate Next(MoveCandidate candidate)
            => new MoveCandidate(candidate.Block, Path.Add(candidate.Path.First));

        internal int Offset => Block.Offset;
        internal short Id => Block.Id; 
        internal short Primary => Block.Primary;

        internal IEnumerable<MoveCandidate> Nexts => Block.Nexts;
    }
}
