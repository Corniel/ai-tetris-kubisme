using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tetris;

namespace Block_Specs
{
    public class Initialize
    {
        [TestCase(ShapeType.I)]
        [TestCase(ShapeType.J)]
        [TestCase(ShapeType.L)]
        [TestCase(ShapeType.O)]
        [TestCase(ShapeType.S)]
        [TestCase(ShapeType.T)]
        [TestCase(ShapeType.Z)]
        public void All_have_height_20(ShapeType type)
        {
        }
    }
}
