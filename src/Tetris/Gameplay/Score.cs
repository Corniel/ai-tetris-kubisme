namespace Tetris.Gameplay
{
    public static class Score
    {
        public static int Classic(int level, Path path, Clearing clearing)
        {
            var score = 1 * path.Downs;

            if (clearing.PerfectClear)
            {
                switch (clearing.Rows)
                {
                    case 1: score += 0800 * level; break;
                    case 2: score += 1200 * level; break;
                    case 3: score += 1800 * level; break;
                    case 4: score += 2000 * level; break;
                }
            }
            else if (clearing.TSpin)
            {
                switch (clearing.Rows)
                {
                    case 0: score += 0400 * level; break;
                    case 1: score += 0800 * level; break;
                    case 2: score += 1200 * level; break;
                    case 3: score += 1600 * level; break;
                }
            }
            else
            {
                switch (clearing.Rows)
                {
                    case 1: score += 0100 * level; break;
                    case 2: score += 0300 * level; break;
                    case 3: score += 0500 * level; break;
                    case 4: score += 0800 * level; break;
                }
            }

            return score;
        }
    }
}
