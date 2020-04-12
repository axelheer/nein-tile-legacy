using System.IO;
using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class GameStateTest
    {
        [Fact]
        public void ShouldNotMove()
        {
            var subject = new GameState(
                new FakeTilesDeck(),
                new FakeTilesArea()
                {
                    OnCanMove = _ => false
                }
            );

            Assert.False(subject.CanMove());

            var actual = subject.Move(MoveDirection.Down);

            Assert.Null(actual.Previous);
            Assert.Equal(subject, actual);
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

            Assert.True(subject.CanMove());

            var actual = subject.Move(MoveDirection.Up);

            Assert.Equal(subject, actual.Previous);
            Assert.Equal(expectedDeck, actual.Deck);
            Assert.Equal(expectedArea, actual.Area);
        }

        [Fact]
        public void ShouldPersist()
        {
            var expected = GameFactory.CreateNew("classic", new GameOptions(4, 4, 4))
                .Move(MoveDirection.Right)
                .Move(MoveDirection.Left)
                .Move(MoveDirection.Up)
                .Move(MoveDirection.Down)
                .Move(MoveDirection.Forward)
                .Move(MoveDirection.Backward);

            using var stream = new MemoryStream();
            expected.Save(stream);
            stream.Position = 0;

            var actual = GameState.Load(stream);

            while (expected != null)
            {
                Assert.NotNull(actual);
                Assert.NotSame(expected, actual);

                Assert.Equal(expected.Deck.Size, actual.Deck.Size);
                for (var deckIndex = 0; deckIndex < expected.Deck.Size; deckIndex++)
                    Assert.Equal(expected.Deck[deckIndex], actual.Deck[deckIndex]);
                Assert.Equal(expected.Area.ColCount, actual.Area.ColCount);
                Assert.Equal(expected.Area.RowCount, actual.Area.RowCount);
                Assert.Equal(expected.Area.LayCount, actual.Area.LayCount);
                for (var colIndex = 0; colIndex < expected.Area.ColCount; colIndex++)
                {
                    for (var rowIndex = 0; rowIndex < expected.Area.RowCount; rowIndex++)
                    {
                        for (var layIndex = 0; layIndex < expected.Area.LayCount; layIndex++)
                        {
                            Assert.Equal(
                                expected.Area[rowIndex, colIndex, layIndex],
                                actual.Area[rowIndex, colIndex, layIndex]);
                        }
                    }
                }

                expected = expected.Previous!;
                actual = actual.Previous!;
            }
        }
    }
}
