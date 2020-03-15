using System;
using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class GameStateTest
    {
        [Fact]
        public void ShouldThrowOnMove()
        {
            var subject = new GameState(
                new FakeTilesDeck(),
                new FakeTilesArea()
                {
                    OnCanMove = _ => false
                }
            );

            var error = Assert.Throws<InvalidOperationException>(() => subject.Move(MoveDirection.Down));

            Assert.Equal("Unable to move into direction 'Down'.", error.Message);
        }

        [Fact]
        public void ShouldMove()
        {
            var expectedTile = new TileInfo(11, 38);
            var expectedDeck = new FakeTilesDeck();
            var expectedArea = new FakeTilesArea();

            var subject = new GameState(
                new FakeTilesDeck()
                {
                    OnShow = () => expectedTile,
                    OnDraw = _ => expectedDeck
                },
                new FakeTilesArea()
                {
                    OnCanMove = _ => true,
                    OnMove = (direction, nextTile) =>
                    {
                        Assert.Equal(MoveDirection.Up, direction);
                        Assert.Equal(expectedTile, nextTile);

                        return expectedArea;
                    }
                }
            );

            var actual = subject.Move(MoveDirection.Up);

            Assert.Equal(expectedDeck, actual.Deck);
            Assert.Equal(expectedArea, actual.Area);
        }
    }
}
