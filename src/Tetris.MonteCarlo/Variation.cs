using System.Linq;
using Tetris.Gameplay;
using Tetris.Generation;

namespace Tetris.MonteCarlo
{
    public class Variation
    {
        public Variation(Blocks blocks, Path path, Field field, int score)
        {
            Blocks = blocks;
            Path = path;
            Field = field;
            InitialScore = score;
        }

        public Blocks Blocks { get; }
        public Path Path { get; }
        public Field Field { get; }
        public int InitialScore { get; }

        public EScore E { get; private set; }

        public void Simulate() => E += Simulate(Field, default, InitialScore);
     
        public int Simulate(Field field, Shape shape, int score)
        {
            var candidates = MoveGenerator.New(field, Blocks.Spawn(shape)).ToArray();

            if (candidates.Length == 0) { return score; }

            var candidate = candidates[0];
            var move = field.Move(candidate.Block);

            return Simulate(
                field: move.Field,
                shape: shape,
                score: score + Score.Classic(1, candidate.Path, move.Clearing));
        }

        
        public static Variation Select(Blocks blocks, Field field, MoveCandidate candidate, int level)
        {
            var move = field.Move(candidate.Block);
            var score = Gameplay.Score.Classic(level, candidate.Path, move.Clearing);
            
            return new Variation(
                blocks: blocks,
                path: candidate.Path,
                field: move.Field,
                score: score);
        }
    }
}
