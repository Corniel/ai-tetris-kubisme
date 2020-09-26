using System.Collections.Generic;
using System.Linq;

namespace Tetris.Navigation
{
    public static class Walker
    {
        /// <summary>Visits all <see cref="Block"/>s that can be visited based
        /// on the <see cref="Blocks.Spawn(ShapeType)"/> blocks.
        /// </summary>
        public static ISet<Block> Visit(Blocks blocks)
        {
            var visited = new HashSet<Block>();

            foreach (var shape in Shape.All.Where(s => s.Rotation == Rotation.None).Select(s => s.Type))
            {
                var block = blocks.Spawn(shape);
                visited.Visit(block);
            }
            return visited;
        }

        private static ISet<Block> Visit(this ISet<Block> visited, Block block)
            => block is Block && visited.Add(block)
            ? visited
                .Visit(block.Down)
                .Visit(block.Left)
                .Visit(block.Right)
                .Visit(block.TurnLeft)
                .Visit(block.TurnRight)
            : visited;
    }
}
