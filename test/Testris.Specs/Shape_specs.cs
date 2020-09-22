using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Tetris;

namespace Shape_specs
{
    public class All
    {
        public static IEnumerable<Shape> Shapes => typeof(Shape)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(f => f.FieldType == typeof(Shape))
            .Select(f => (Shape)f.GetValue(null));

        [TestCaseSource(nameof(Shapes))]
        public void Have_size_of_four(Shape shape)
        {
            Assert.AreEqual(4, shape.Count);
        }

        [TestCase(ShapeType.J)]
        [TestCase(ShapeType.I)]
        [TestCase(ShapeType.L)]
        [TestCase(ShapeType.S)]
        [TestCase(ShapeType.T)]
        [TestCase(ShapeType.Z)]
        public void Except_O_have_four_variations(ShapeType type)
        {
            Assert.AreEqual(4, Shapes.Count(shape => shape.Type == type));
        }

        [Test]
        public void Have_unique_combination_of_type_and_rotation()
        {
            var all = Shapes.ToArray();
            Assert.AreEqual(all.Distinct(new TypeAndRotationComparer()), all);
        }

        [Test]
        public void Have_unique_shapes()
        {
            var all = Shapes.ToArray();
            Assert.AreEqual(all.Distinct(new ShapeComparer()), all);
        }

        private class TypeAndRotationComparer : IEqualityComparer<Shape>
        {
            public bool Equals([DisallowNull] Shape x, [DisallowNull] Shape y)
                => x.Type == y.Type
                && x.Rotation == y.Rotation;
            public int GetHashCode(Shape obj) => 0;
        }

        private class ShapeComparer : IEqualityComparer<Shape>
        {
            public bool Equals([DisallowNull] Shape x, [DisallowNull] Shape y)
                => Enumerable.SequenceEqual(x, y);

            public int GetHashCode(Shape obj) => 0;
        }
    }

    public class O
    {
        [Test]
        public void Has_one_variation()
        {
            Assert.AreEqual(1, All.Shapes.Count(shape => shape.Type == ShapeType.O));
        }
    }
}
