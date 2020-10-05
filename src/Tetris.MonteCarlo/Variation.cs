using Tetris.Generation;

namespace Tetris.MonteCarlo
{
    public class Variation
    {
        public Variation(Path path, Field field, int score)
        {
            Path = path;
            Field = field;
            Score = score;
        }

        public Path Path { get; }
        public Field Field { get; }
        public int Score { get; }

        public static Variation Select(Field field, MoveCandidate candidate, int level)
        {
            var move = field.Move(candidate.Block);
            var score = Gameplay.Score.Classic(level, candidate.Path, move.Clearing);
            
            return new Variation(
                path: candidate.Path,
                field: move.Field,
                score: score);
        }
    }
}
