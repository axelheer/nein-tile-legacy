using System;
using NeinTile.Editions;
using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class TilesAreaTest
    {
        [Fact]
        public void ShouldHandleNull()
        {
            var tiles = Assert.Throws<ArgumentNullException>(()
                => new TilesArea(null!, new DefaultDealer(), new FakeMerger()));

            Assert.Equal(nameof(tiles), tiles.ParamName);

            var dealer = Assert.Throws<ArgumentNullException>(()
                => new TilesArea(new Tiles(0, 0, 0), null!, new FakeMerger()));

            Assert.Equal(nameof(dealer), dealer.ParamName);

            var merger = Assert.Throws<ArgumentNullException>(()
                => new TilesArea(new Tiles(0, 0, 0), new DefaultDealer(), null!));

            Assert.Equal(nameof(merger), merger.ParamName);
        }

        [Fact]
        public void ShouldCanMove()
        {
            var tiles = new Tiles(2, 2, 1);
            tiles[0, 0, 0] = new Tile(1, 0);
            tiles[1, 0, 0] = new Tile(1, 0);
            tiles[0, 1, 0] = new Tile(3, 0);
            tiles[1, 1, 0] = new Tile(4, 0);

            var subject = new TilesArea(
                tiles,
                new DefaultDealer(),
                new FakeMerger()
                {
                    OnCanMerge = (source, target) => source == target
                }
            );

            var actual = subject.CanMove(MoveDirection.Right);

            Assert.True(actual);
            Assert.Equal(1, subject.Tiles[0, 0, 0].Value);
            Assert.Equal(1, subject.Tiles[1, 0, 0].Value);
            Assert.Equal(3, subject.Tiles[0, 1, 0].Value);
            Assert.Equal(4, subject.Tiles[1, 1, 0].Value);
        }

        [Fact]
        public void ShouldNotCanMove()
        {
            var tiles = new Tiles(2, 2, 1);
            tiles[0, 0, 0] = new Tile(1, 0);
            tiles[1, 0, 0] = new Tile(2, 0);
            tiles[0, 1, 0] = new Tile(3, 0);
            tiles[1, 1, 0] = new Tile(4, 0);

            var subject = new TilesArea(
                tiles,
                new DefaultDealer(),
                new FakeMerger()
                {
                    OnCanMerge = (source, target) => source == target
                }
            );

            var actual = subject.CanMove(MoveDirection.Up);

            Assert.False(actual);
            Assert.Equal(1, subject.Tiles[0, 0, 0].Value);
            Assert.Equal(2, subject.Tiles[1, 0, 0].Value);
            Assert.Equal(3, subject.Tiles[0, 1, 0].Value);
            Assert.Equal(4, subject.Tiles[1, 1, 0].Value);
        }

        [Fact]
        public void ShouldMove()
        {
            var tiles = new Tiles(2, 2, 1);
            tiles[0, 0, 0] = new Tile(1, 0);
            tiles[1, 0, 0] = new Tile(1, 0);
            tiles[0, 1, 0] = new Tile(3, 0);
            tiles[1, 1, 0] = new Tile(4, 0);

            var subject = new TilesArea(
                tiles,
                new DefaultDealer(),
                new FakeMerger()
                {
                    OnCanMerge = (source, target) => source == target
                }
            );

            var actual = subject.Move(MoveDirection.Right, false, new Tile(7, 0));

            Assert.Equal(7, actual.Tiles[0, 0, 0].Value);
            Assert.Equal(0, actual.Tiles[1, 0, 0].Value);
            Assert.Equal(3, actual.Tiles[0, 1, 0].Value);
            Assert.Equal(4, actual.Tiles[1, 1, 0].Value);
        }
    }
}
