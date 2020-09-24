using System;

namespace Tetris
{
    internal class SuperRotationSystem : RotationSystem
    {
        private readonly Blocks blocks;

        internal SuperRotationSystem(Blocks blocks)
        {
            this.blocks = blocks;
        }

        public override Block TurnLeft(Block block) => Turn(block, -1);
        public override Block TurnRight(Block block) => Turn(block, +1);

        private Block Turn(Block block, int rotation)
        {
            if (block.Shape.Type == ShapeType.O) return null;

            var source = Offset(block);
            var target = Offset(block, rotation);

            var col = target.Column - source.Column;
            var flr = target.Floor - source.Floor;

            col += block.Column;
            flr += block.Offset;

            //if(block.Shape.Type == ShapeType.I && 
            //    block.Shape.Rotation == default &&
            //    block.Column == 0 &&
            //    block.Offset == 10)
            //{

            //}

            return col < 0 || col > 9 || flr < 0 || flr > 19
                ? null
                : blocks.Select(col, flr, block.Shape.Type, block.Shape.Rotation, rotation);
        }

        private static Offset Offset(Block block, int add = 0) 
            => Offset(block.Shape.Type, block.Shape.Rotation, add);

        private static Offset Offset(ShapeType shape, Rotation rotation, int add) => offsets
           [(int)shape]
           [((int)rotation + add + 4) % 4];

        private static readonly Offset[][] offsets = new Offset[][]
        {
            /*I*/ new Offset[]{ Off(0, 2), Off(2, 0), Off(0, 1), Off(1, 0) },
            /*J*/ new Offset[]{ Off(0, 1), Off(1, 0), Off(0, 0), Off(0, 1) },
            /*L*/ new Offset[]{ Off(0, 1), Off(1, 0), Off(0, 0), Off(0, 1) },
            /*0*/ null,
            /*S*/ new Offset[]{ Off(0, 1), Off(1, 0), Off(0, 0), Off(0, 1) },
            /*T*/ new Offset[]{ Off(0, 1), Off(1, 0), Off(0, 0), Off(0, 1) },
            /*Z*/ new Offset[]{ Off(0, 1), Off(1, 0), Off(0, 0), Off(0, 1) },
        };
        
        private static Offset Off(int column, int floor) => new Offset(column, floor);
    }
}
