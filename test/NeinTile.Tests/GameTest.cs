using System;
using System.IO;
using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class GameTest
    {
        [Fact]
        public void ShouldHandleNull()
        {
            var deck = Assert.Throws<ArgumentNullException>(()
                => new Game(null!, FakeTilesArea.Create(), false));

            Assert.Equal(nameof(deck), deck.ParamName);

            var area = Assert.Throws<ArgumentNullException>(()
                => new Game(FakeTilesDeck.Create(), null!, false));

            Assert.Equal(nameof(area), area.ParamName);

            var subject = new Game(
                FakeTilesDeck.Create(),
                FakeTilesArea.Create(),
                false
            );

            var inputStream = Assert.Throws<ArgumentNullException>(()
                => subject.Save(null!));

            Assert.Equal(nameof(inputStream), inputStream.ParamName);

            var outputStream = Assert.Throws<ArgumentNullException>(()
                => Game.Load(null!));

            Assert.Equal(nameof(outputStream), outputStream.ParamName);
        }

        [Fact]
        public void ShouldNotMove()
        {
            var subject = new Game(
                FakeTilesDeck.Create(),
                FakeTilesArea.Create(),
                false
            );

            Assert.False(subject.CanMove());

            var actual = subject.Move(MoveDirection.Down);

            Assert.Null(actual.Previous);
            Assert.Equal(subject, actual);
        }

        [Fact]
        public void ShouldMove()
        {
            var subject = new Game(
                FakeTilesDeck.Create(),
                FakeTilesArea.Create(merger: new FakeMerger()
                {
                    OnCanMerge = (_, __) => true
                }),
                false
            );

            Assert.True(subject.CanMove());

            var actual = subject.Move(MoveDirection.Up);

            Assert.Equal(subject, actual.Previous);
        }

        [Fact]
        public void ShouldPersist()
        {
            var expected = new GameMaker().MakeGame()
                .Move(MoveDirection.Right)
                .Move(MoveDirection.Left)
                .Move(MoveDirection.Up)
                .Move(MoveDirection.Down)
                .Move(MoveDirection.Front)
                .Move(MoveDirection.Back);

            using var stream = new MemoryStream();
            expected.Save(stream);
            stream.Position = 0;

            var actual = Game.Load(stream);

            while (expected != null)
            {
                Assert.NotNull(actual);
                Assert.NotSame(expected, actual);

                Assert.Equal(expected.Deck.Tile, actual.Deck.Tile);
                Assert.Equal(expected.Deck.Hint, actual.Deck.Hint);
                Assert.Equal(expected.Area.Tiles.ColCount, actual.Area.Tiles.ColCount);
                Assert.Equal(expected.Area.Tiles.RowCount, actual.Area.Tiles.RowCount);
                Assert.Equal(expected.Area.Tiles.LayCount, actual.Area.Tiles.LayCount);
                for (var colIndex = 0; colIndex < expected.Area.Tiles.ColCount; colIndex++)
                {
                    for (var rowIndex = 0; rowIndex < expected.Area.Tiles.RowCount; rowIndex++)
                    {
                        for (var layIndex = 0; layIndex < expected.Area.Tiles.LayCount; layIndex++)
                        {
                            Assert.Equal(
                                expected.Area.Tiles[rowIndex, colIndex, layIndex],
                                actual.Area.Tiles[rowIndex, colIndex, layIndex]);
                        }
                    }
                }

                expected = expected.Previous!;
                actual = actual.Previous!;
            }
        }
    }
}
