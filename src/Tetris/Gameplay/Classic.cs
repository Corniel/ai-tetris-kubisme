using System;

namespace Tetris.Gameplay
{
    public class Classic
    {
        private readonly RandomGenerator rnd;
        private Classic(
            RandomGenerator generator,
            TimeSpan time,
            Field field,
            Blocks blocks,
            Shape current,
            Shape next,
            long score,
            int level,
            int moves)
            {
                rnd = generator;
                Time = time;
                Field = field;
                Blocks = blocks;
                Current = current;
                Next = next;
                Score = score;
                Level = level;
                Moves = moves;
            }

            public TimeSpan Time { get; }
            public Field Field { get; }
            public Blocks Blocks { get; }
            public Shape Current { get; }
            public Shape Next { get; }
            public long Score { get; }
            public int Level { get; }
            public int Moves { get; }

        public Classic Move(Steps steps)
        {
            var block = Blocks.Spawn(Current);
            var move = Field.Move(block, steps);
            var field = move.Field;
            var next = rnd.Next();
            var score = Score + move.Clearing.Rows * (Level + 1);
            var moves = Moves + 1;
            var level = Math.Max(Level, moves / 10);

            return new Classic(
                generator: rnd,
                time: Time,
                field: field,
                blocks: Blocks,
                current: Next,
                next: next,
                score: score,
                level: level,
                moves: moves);
        }

        public static Classic Start(
            int startLevel = 0, 
            Blocks blocks = null,
            RandomGenerator generator = null)
        {
            generator ??= new RandomGenerator();
            blocks ??= Blocks.Init();
            return new Classic(
                generator: generator,
                time: TimeSpan.FromSeconds(0.5),
                field: Field.New(),
                blocks: blocks,
                current: generator.Next(),
                next: generator.Next(),
                score: 0,
                level: startLevel,
                moves: 0);
        }
            }
}
