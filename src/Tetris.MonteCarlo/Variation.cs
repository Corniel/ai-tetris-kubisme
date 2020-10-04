using Tetris.Generation;

namespace Tetris.MonteCarlo
{
    public class Variation
    {
        public Variation(Steps steps, Field field, int score)
        {
            Steps = steps;
            Field = field;
            Score = score;
        }

        public Steps Steps { get; }
        public Field Field { get; }
        public int Score { get; }

        public static Variation Select(Field field, MoveCandidate candidate, int level)
        {
            var move = field.Move(candidate.Block);
            var score = Gameplay.Score.Classic(level, candidate.Steps, move.Clearing);
            
            return new Variation(
                steps: candidate.Steps,
                field: move.Field,
                score: score);
        }
    }
}
