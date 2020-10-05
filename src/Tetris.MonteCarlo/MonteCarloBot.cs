using System.Linq;
using System.Text.RegularExpressions;
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

            var candidates = generator
                .Select(candidate =>Variation.Select(game.Field, candidate, game.Level))
                .ToArray();

            return candidates[0].Path;
        }
    }

}
