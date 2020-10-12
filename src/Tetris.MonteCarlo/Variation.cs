using System;
using System.Linq;
using Tetris.Gameplay;
using Tetris.Generation;
using Tetris.Randomization;
using Troschuetz.Random.Generators;
using static System.FormattableString;

namespace Tetris.MonteCarlo
{
    public class Variation : IComparable<Variation>
    {
        public Variation(Blocks blocks, Path path, Field field, int score)
        {
            Blocks = blocks;
            Path = path;
            Field = field;
            InitialScore = score;
            Rnd = new MT19937Generator();
            Shape = new SimpleGenerator(Rnd);
        }

        public Blocks Blocks { get; }
        public Path Path { get; }
        public Field Field { get; }
        public int InitialScore { get; }
        public Expected E { get; private set; }
        private MT19937Generator Rnd { get; }
        private SimpleGenerator Shape { get; }

        public void Simulate(Shape shape) => E += Simulate(Field, shape, InitialScore);

        public int Simulate(Field field, Shape shape, int score)
        {
            var candidates = MoveGenerator.New(field, Blocks.Spawn(shape)).ToArray();

            if (candidates.Length == 0)
            {
                return score;
            }
            else
            {
                var candidate = candidates[Rnd.Next(candidates.Length)];
                var move = field.Move(candidate.Block);

                return Simulate(
                    field: move.Field,
                    shape: Shape.Next(),
                    score: score + Score.Classic(1, candidate.Path, move.Clearing));
            }
        }

        public override string ToString() => Invariant($"E: {E}");

        public static Variation Select(Blocks blocks, Field field, MoveCandidate candidate, int level)
        {
            var move = field.Move(candidate.Block);
            var score = Score.Classic(level, candidate.Path, move.Clearing);

            return new Variation(
                blocks: blocks,
                path: candidate.Path,
                field: move.Field,
                score: score);
        }

        public int CompareTo(Variation other) => other.E.CompareTo(E);
    }
}
