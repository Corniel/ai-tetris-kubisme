namespace Tetris
{
    internal class SuperRotationSystem : RotationSystem
    {
        private readonly Rows rows;

        internal SuperRotationSystem(Rows rows)
        {
            this.rows = rows;
        }

        public override Block TurnLeft(Block block) => Turn(block, -1);
        public override Block TurnRight(Block block) => Turn(block, +1);

        private Block Turn(Block block, int rotation)
        {
            if (block.Shape == Shape.O) return null;

            var source = Offset(block);
            var target = Offset(block, rotation);

            var col = target.Column - source.Column;
            var flr = target.Floor - source.Floor;

            col += block.Column;
            flr += block.Offset;

            return rows.Block(block.Shape, block.Rotation.Rotate(rotation), col, flr);
        }

        private static Offset Offset(Block block, int add = 0) 
            => Offset(block.Shape, block.Rotation, add);

        private static Offset Offset(Shape shape, Rotation rotation, int add) => offsets
           [(int)shape]
           [rotation.Rotate(add)];

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
