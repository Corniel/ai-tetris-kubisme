using System;
using Troschuetz.Random;
using Troschuetz.Random.Generators;

namespace Tetris.Gameplay
{
    public class Classic
    {
        private readonly IGenerator rnd;
        private Classic(
            IGenerator generator,
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
            var next = NextShape(rnd);
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
            IGenerator generator = null)
        {
            generator ??= new MT19937Generator();
            blocks ??= Blocks.Init();
            return new Classic(
                generator: generator,
                time: TimeSpan.FromSeconds(0.5),
                field: Field.New(),
                blocks: blocks,
                current: NextShape(generator),
                next: NextShape(generator),
                score: 0,
                level: startLevel,
                moves: 0);
        }

        private static Shape NextShape(IGenerator generator) => (Shape)generator.Next(7);
    }
}
