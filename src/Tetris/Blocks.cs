using SmartAss;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tetris
{
    public class Blocks : IEnumerable<Block>
    {
        public Block Select(int col, int offset, ShapeType shape, Rotation rotation)
            => items[col][offset][(int)shape][(int)rotation];

        public static Blocks Init()
        {
            var blocks = new Blocks();

            foreach (var block in Shape.All.Select(shape => new Block(shape)))
            {
                blocks.Init(block);
            }

            return blocks;
        }

        private Block Init(Block block)
        {
            var existing = Select(block.Column, block.Offset, block.Shape.Type, block.Shape.Rotation);
            if (existing is Block)
            {
                return existing;
            }
            else
            {
                Set(block);

                if (block.Width < 10)
                {
                    var right = Init(block.CreateRight());
                    block.Right = right;
                    right.Left = block;
                }
                if (block.Height < 20)
                {
                    var up = Init(block.CreateUp());
                    up.Down = block;
                }
                return block;
            }
        }

        private void Set(Block block)
            => items
              [block.Column]
              [block.Offset]
              [(int)block.Shape.Type]
              [(int)block.Shape.Rotation] = block;

        public IEnumerator<Block> GetEnumerator() => items
            .SelectMany(m => m)
            .SelectMany(n => n)
            .SelectMany(o => o)
            .Where(b => b != null)
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Block[][][][] items = Jagged.Array<Block>(10, 20, 7, 4);
    }
}
