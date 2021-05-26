using System.Linq;

namespace Tetris
{
    public partial class Blocks
    {
        public static Blocks Init() => Init(Rows.All(), 20, null);

        public static Blocks Init(Rows rows, int height, RotationSystem rotationSystem)
        {
            var spawns = new[]
            {
                rows.Block(Shape.I, default, 4, height - 1),
                rows.Block(Shape.J, default, 4, height - 1),
                rows.Block(Shape.L, default, 4, height - 1),
                rows.Block(Shape.O, default, 5, height - 1),
                rows.Block(Shape.S, default, 4, height - 1),
                rows.Block(Shape.T, default, 4, height - 1),
                rows.Block(Shape.Z, default, 4, height - 1),
            };

            var blocks = new Blocks(spawns, rows, rotationSystem ?? RotationSystem.Srs(rows));

            foreach (var block in blocks.spawns)
            {
                blocks.Init(block);
            }

            foreach (var shape in Shapes.All)
            {
                short id = 0;
                foreach (var block in blocks.Where(b => b.Shape == shape))
                {
                    block.Id = id++;
                    blocks.Count++;
                }
            }
            foreach(var block in blocks)
            {
                var primary = block.Shape.HasPrimary()
                    ? blocks
                        .Select(
                            col: block.Column,
                            offset: block.Offset,
                            shape: block.Shape,
                            rotation: block.Rotation.Primary()) ?? block
                    : block;

                block.Primary = primary.Id;
            }

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
            : Select(block.Column, block.Offset - 1, block.Shape, block.Rotation)
            ?? rows.Block(block.Shape, block.Rotation, block.Column, block.Offset - 1);

        private Block Left(Block block)
            => block.Column == 0
            ? null
            : Select(block.Column - 1, block.Offset, block.Shape, block.Rotation)
            ?? rows.Block(block.Shape, block.Rotation, block.Column - 1, block.Offset);

        private Block Right(Block block)
           => block.Column >= rows.Columns(block.Shape, block.Rotation) - 1
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
