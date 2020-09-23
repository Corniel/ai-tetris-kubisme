using SmartAss;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tetris
{
    public class Blocks : IEnumerable<Block>
    {
        public Block Spawn(ShapeType shape)
            => shape switch
            {
                ShapeType.J => Select(3, 17, ShapeType.J, default),
                ShapeType.L => Select(3, 17, ShapeType.L, default),
                ShapeType.S => Select(3, 17, ShapeType.T, default),
                ShapeType.T => Select(3, 17, ShapeType.S, default),
                ShapeType.Z => Select(3, 17, ShapeType.Z, default),

                ShapeType.O => Select(4, 17, ShapeType.O, default),
                ShapeType.I => Select(3, 18, ShapeType.I, default),
                _ => throw new NotSupportedException(),
            };

        public Block Select(int col, int offset, ShapeType shape, Rotation rotation)
           => Select(col, offset, shape, rotation, 0);

        public Block Select(int col, int offset, ShapeType shape, Rotation rotation, int add)
            => items[col][offset][(int)shape][((int)rotation + add) % 4];

        public static Blocks Init() => Init(null);

        public static Blocks Init(RotationSystem rotationSystem)
        {
            var blocks = new Blocks();

            foreach (var block in Shape.All.Select(shape => new Block(shape)))
            {
                blocks.Init(block);
            }

            rotationSystem ??= RotationSystem.Srs(blocks);

            foreach (var block in blocks)
            {
                block.TurnLeft = rotationSystem.TurnLeft(block);
                block.TurnRight = rotationSystem.TurnRight(block);
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
