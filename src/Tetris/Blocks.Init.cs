namespace Tetris
{
    public partial class Blocks
    {
        public static Blocks Init() => Init(Rows.All(), null);

        public static Blocks Init(Rows rows, RotationSystem rotationSystem)
        {
            var spawns = new Block[]
            {
                rows.Block(Shape.I, default, 4, 18),
                rows.Block(Shape.J, default, 4, 17),
                rows.Block(Shape.L, default, 4, 17),
                rows.Block(Shape.O, default, 5, 17),
                rows.Block(Shape.S, default, 4, 17),
                rows.Block(Shape.T, default, 4, 17),
                rows.Block(Shape.Z, default, 4, 17),
            };

            var blocks = new Blocks(spawns, rows, rotationSystem ?? RotationSystem.Srs(rows));

            foreach (var block in blocks.spawns)
            {
                blocks.Init(block);
            }
            var id = 0;
            foreach(var block in blocks)
            {
                block.Id = id++;
            }
            blocks.Count = id;
            return blocks;
        }

        private Block Init(Block block)
        {
            if (block is null) { return null; }

            var existing = Select(block.Column, block.Offset, block.Shape, block.Rotation);
            if (existing is Block)
            {
                return existing;
            }
            else
            {
                Set(block);

                block.Down = Init(Down(block));
                block.Left = Init(Left(block));
                block.Right = Init(Right(block));
                block.TurnLeft = Init(rotation.TurnLeft(block));
                block.TurnRight = Init(rotation.TurnRight(block));
                block.InitNexts();

                return block;
            }
        }

        private Block Down(Block block) 
            => block.Offset == 0
            ? null
            : Select(block.Column, block.Offset -1, block.Shape, block.Rotation)
            ?? rows.Block(block.Shape, block.Rotation, block.Column, block.Offset - 1);

        private Block Left(Block block)
            => block.Column == 0
            ? null
            : Select(block.Column - 1, block.Offset, block.Shape, block.Rotation)
            ?? rows.Block(block.Shape, block.Rotation, block.Column -1, block.Offset);

        private Block Right(Block block)
           => block.Column >= rows.Columns(block.Shape, block.Rotation) -1
           ? null
           : Select(block.Column + 1, block.Offset, block.Shape, block.Rotation)
           ?? rows.Block(block.Shape, block.Rotation, block.Column + 1, block.Offset);

        private void Set(Block block)
            => items
              [block.Column]
              [block.Offset]
              [(int)block.Shape]
              [(int)block.Rotation] = block;
    }
}
