using System.Diagnostics;
using System.Linq;
using Tetris.Gameplay;
using Tetris.Generation;

namespace Tetris.MonteCarlo
{
    public class MonteCarloBot
    {
        public Path Play(Classic game)
        {
            var sw = Stopwatch.StartNew();

            var block = game.Blocks.Spawn(game.Current);

            var generator = MoveGenerator.New(game.Field, block);

            var variations = generator
                .Select(candidate => Variation.Select(game.Blocks, game.Field, candidate, game.Level))
                .ToList();

            while (sw.Elapsed < game.Time)
            {
                foreach (var variation in variations)
                {
                    variation.Simulate(game.Next);
                }
            }
            variations.Sort();

            return variations[0].Path;
        }
    }

}
