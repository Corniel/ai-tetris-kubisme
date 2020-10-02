namespace Tetris
{
    public abstract class RotationSystem
    {
        public static RotationSystem Srs(Rows rows) => new SuperRotationSystem(rows);

        public abstract Block TurnLeft(Block block);
        public abstract Block TurnRight(Block block);
    }
}
