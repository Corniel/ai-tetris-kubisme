using NUnit.Framework;
using System;
using Tetris.Gameplay;
using Tetris.MonteCarlo;

namespace MonteCarlo_specs
{
    public class Classic_Game
    {
        [Test]
        public void Play()
        {
            var game = Classic.Start();
            var bot = new MonteCarloBot();

            var turns = 0;
            while (game.Field.Filled < 18)
            {
                var move = bot.Play(game);
                game = game.Move(move);
                Console.WriteLine($"{++turns}: {game.Field.Filled}");
            }
        }
    }
}
