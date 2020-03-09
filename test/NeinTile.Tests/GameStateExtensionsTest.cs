using System;
using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class GameStateExtensionsTest
    {
        [Fact]
        public void ShouldThrowOnPreview()
        {
            var subject = default(GameState);

            var error = Assert.Throws<ArgumentNullException>(() => subject.Preview());

            Assert.Equal("state", error.ParamName);
        }

        [Fact]
        public void ShouldPreview()
        {
            var expected = new TileInfo[7];

            var subject = new GameState(
                new FakeTilesDeck()
                {
                    OnPreview = () => expected
                },
                new FakeTilesArea());

            var actual = subject.Preview();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowOnCanMove()
        {
            var subject = default(GameState);

            var error = Assert.Throws<ArgumentNullException>(() => subject.CanMove(MoveDirection.Up));

            Assert.Equal("state", error.ParamName);
        }

        [Fact]
        public void ShouldCanMove()
        {
            var subject = new GameState(
                new FakeTilesDeck(),
                new FakeTilesArea()
                {
                    OnCanMove = direction => direction == MoveDirection.Up
                });

            var actual = subject.CanMove(MoveDirection.Up);

            Assert.True(actual);
        }
    }
}
