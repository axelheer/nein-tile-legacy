using System;
using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class TilesDeckTest
    {
        [Fact]
        public void ShouldHandleNull()
        {
            var mixer = Assert.Throws<ArgumentNullException>(()
                => new TilesDeck(null!, new FakeLottery()));

            Assert.Equal(nameof(mixer), mixer.ParamName);

            var lottery = Assert.Throws<ArgumentNullException>(()
                => new TilesDeck(new FakeMixer(), null!));

            Assert.Equal(nameof(lottery), lottery.ParamName);
        }

        [Fact]
        public void ShouldHintBonus()
        {
            var expected = new TileHint(
                new Tile(1, 2),
                new Tile(2, 3),
                new Tile(3, 4)
            );

            var subject = new TilesDeck(
                new FakeMixer(),
                new FakeLottery()
                {
                    OnDraw = _ => (expected, expected.Second)
                }
            );

            var actual = subject.Next(0).Hint;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldHintNextTile()
        {
            var expected = new TileHint(
                new Tile(1, 2)
            );

            var subject = new TilesDeck(
                new FakeMixer()
                {
                    OnMix = () => new[] { expected.First }
                },
                new FakeLottery()
            );

            var actual = subject.Hint;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldShowBonus()
        {
            var expected = new TileHint(
                new Tile(1, 2),
                new Tile(2, 3),
                new Tile(3, 4)
            );

            var subject = new TilesDeck(
                new FakeMixer(),
                new FakeLottery()
                {
                    OnDraw = _ => (expected, expected.Second)
                }
            );

            var actual = subject.Next(0).Tile;

            Assert.Equal(expected.Second, actual);
        }

        [Fact]
        public void ShouldShowNextTile()
        {
            var expected = new Tile(1, 2);
            var unexpected = new Tile(2, 3);

            var subject = new TilesDeck(
                new FakeMixer()
                {
                    OnMix = () => new[] { expected, unexpected }
                },
                new FakeLottery()
            );

            var actual = subject.Tile;

            Assert.Equal(expected, actual);
        }
    }
}
