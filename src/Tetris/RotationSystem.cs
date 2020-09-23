namespace Tetris
{
    public abstract class RotationSystem
    {
        public static RotationSystem Srs(Blocks blocks) => new SuperRotationSystem(blocks);

        public abstract Block TurnLeft(Block block);
        public abstract Block TurnRight(Block block);
    }
}
