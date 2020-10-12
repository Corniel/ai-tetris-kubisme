using NUnit.Framework;
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
            
            while (true)
            {
                var move = bot.Play(game);
                game = game.Move(move);
            }
        }
    }
}
