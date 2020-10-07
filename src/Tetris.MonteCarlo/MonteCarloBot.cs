using System.Linq;
using Tetris.Gameplay;
using Tetris.Generation;

namespace Tetris.MonteCarlo
{
    public class MonteCarloBot
    {
        public Path Play(Classic game)
        {
            var block = game.Blocks.Spawn(game.Current);

            var generator = MoveGenerator.New(game.Field, block);

            var variations = generator
                .Select(candidate =>Variation.Select(game.Blocks, game.Field, candidate, game.Level))
                .ToArray();

            foreach(var variation in variations)
            {
                variation.Simulate();
            }

            return variations[0].Path;
        }
    }

}
