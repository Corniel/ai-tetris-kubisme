using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    public partial class Block
    {
        public static readonly Block O = Init(Shape.O);
        public static readonly Block I = Init(Shape.I, Shape.I_R, Shape.I_U, Shape.I_L);
        public static readonly Block J = Init(Shape.J, Shape.J_R, Shape.J_U, Shape.J_L);
        public static readonly Block L = Init(Shape.L, Shape.L_R, Shape.L_U, Shape.L_L);
        public static readonly Block S = Init(Shape.S, Shape.S_R, Shape.S_U, Shape.S_L);
        public static readonly Block T = Init(Shape.T, Shape.T_R, Shape.T_U, Shape.T_L);
        public static readonly Block Z = Init(Shape.Z, Shape.Z_R, Shape.Z_U, Shape.Z_L);

        public static Block Select(ShapeType type)
           => type switch
           {
               ShapeType.I => I,
               ShapeType.J => J,
               ShapeType.L => L,
               ShapeType.O => O,
               ShapeType.S => S,
               ShapeType.T => T,
               ShapeType.Z => Z,
               _ => throw new NotSupportedException(),
           };

        private static Block Init(Shape @default)
        {
            return Init(New(@default), new Dictionary<BlockId, Block>());
        }

        private static Block Init(Shape @default, Shape right, Shape uturn, Shape left)
        {
            var lookup = new Dictionary<BlockId, Block>();
            var initial = New(@default);
            Init(initial, lookup);
            Init(New(right), lookup);
            Init(New(uturn), lookup);
            Init(New(left), lookup);

            var existing = lookup.Values.ToArray();

            foreach (var item in existing)
            {
                var id = item.Id;
                var rotation = (Rotation)(((int)id.Rotation + 1) % 4);
                var rightId = new BlockId(item.Id.Shape, rotation, id.Column, id.Offset);

                if (lookup.TryGetValue(rightId, out Block rotated))
                {
                    item.TurnRight = rotated;
                    rotated.TurnLeft = item;
                }
            }
            return initial;
        }

        private static Block Init(Block block, Dictionary<BlockId, Block> lookup)
        {
            if(lookup.TryGetValue(block.Id, out Block existing))
            {
                return existing;
            }
            lookup[block.Id] = block;

            if (block.Offset > 0)
            {
                block.Down = Init(block.InitDown(), lookup);
            }
            if (block.Column + block.Shape.Left + block.Shape.Width < 10)
            {
                var right = Init(block.InitRight(), lookup);
                block.Right = right;
                right.Left = block;
            }

            return block;
        }

        private static Block New(Shape shape) 
            => new Block(
                shape: shape,
                rs: shape.Select(r => r.Left(shape.Left)).ToArray(),
                column: -shape.Left, 
                offset: 20 - shape.Height - shape.Top);
        private Block InitDown() => new Block(Shape, rows, Column, Offset - 1);
        private Block InitRight() => new Block(Shape, rows.Select(r => r.Right()).ToArray(), Column + 1, Offset);
    }
}
