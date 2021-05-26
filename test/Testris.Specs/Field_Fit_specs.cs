using NUnit.Framework;
using Testris.Specs;
using Tetris;

namespace Field_Fit_specs
{
    public class Fits_When
    {
        [Test]
        public void Block_hangs_on_other()
        {
            Block block = Data.Block(
                Shape.Z,
                Rotation.None,
                1,
                3,
                0b_01100_00000,
                0b_00110_00000);


            var field = Field.New(
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_11000_00000,
                0b_11000_00000,
                0b_11000_00000,
                0b_11101_00000);

            var fit = field.Fits(block);
            Assert.AreEqual(Fit.True, fit);
        }

        [Test]
        public void Block_leans_on_floor()
        {
            Block block = Data.Block(
                Shape.L,
                Rotation.None,
                0,
                3,
                0b_00100_00000,
                0b_11100_00000);


            var field = Field.New(
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_01100_00000,
                0b_00110_00000,
                0b_11101_00000);

            var fit = field.Fits(block);
            Assert.AreEqual(Fit.True, fit);
        }

        [Test]
        public void Block_leans_on_bottom()
        {
            Block block = Data.Block(
                Shape.L,
                Rotation.None,
                0,
                0,
                0b_00100_00000,
                0b_11100_00000);


            var field = Field.New(
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00010,
                0b_00000_00010,
                0b_00000_00011);

            var fit = field.Fits(block);
            Assert.AreEqual(Fit.True, fit);
        }
    }

    public class Maybe_When
    {
        [Test]
        public void Height_block_higher_height_field()
        {
            Block block = Data.Block(
                Shape.L,
                Rotation.None,
                0,
                19,
                0b_00100_00000,
                0b_11100_00000);


            var field = Field.New(
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00110_00000,
                0b_11101_00000);

            var fit = field.Fits(block);
            Assert.AreEqual(Fit.Maybe, fit);
        }

        [Test]
        public void Offset_Bigger_then_Filled()
        {
            Block block = Data.Block(
                Shape.L,
                Rotation.None,
                0,
                3,
                0b_00100_00000,
                0b_11100_00000);


            var field = Field.New(
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00110_00000,
                0b_11101_00000);

            var fit = field.Fits(block);
            Assert.AreEqual(Fit.Maybe, fit);
        }

        [Test]
        public void No_floor_detected()
        {
            Block block = Data.Block(
                Shape.L,
                Rotation.None,
                0,
                3,
                0b_00100_00000,
                0b_11100_00000);


            var field = Field.New(
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00001,
                0b_00000_00001,
                0b_00110_00011,
                0b_11101_00011);

            var fit = field.Fits(block);
            Assert.AreEqual(Fit.Maybe, fit);
        }
    }

    public class No_Fit_When
    {
        [Test]
        public void Height_block_overlaps_row()
        {
            Block block = Data.Block(
                Shape.L,
                Rotation.None,
                0,
                1,
                0b_00100_00000,
                0b_11100_00000);


            var field = Field.New(
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,

                0b_00000_00000,
                0b_00000_00000,
                0b_00000_00000,
                0b_00110_00000,
                0b_11101_00000);

            var fit = field.Fits(block);
            Assert.AreEqual(Fit.False, fit);
        }
    }
}
