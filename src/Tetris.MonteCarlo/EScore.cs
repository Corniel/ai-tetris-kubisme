namespace Tetris.MonteCarlo
{
    public readonly struct EScore
    {
        private readonly int count;
        private readonly long total;

        private EScore(int cnt, long scr)
        {
            count = cnt;
            total = scr;
        }

        public double Value => count == 0 ? 0 : total / ((double)count);

        public EScore Add(int score) => new EScore(count + 1, score + total);
   
        public override string ToString() => $"{Value:#,##0.0}, Count: {count}";

        public static EScore operator +(EScore score, int value) => score.Add(value);
    }
}
