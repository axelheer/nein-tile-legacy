using System;
using System.Runtime.InteropServices;
using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class PrinterTest
    {
        private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        private readonly Tiles tiles;

        public PrinterTest()
        {
            tiles = new Tiles(3, 3, 1);

            tiles[0, 0, 0] = new Tile(1, 0);
            tiles[1, 0, 0] = new Tile(4, 0);
            tiles[2, 0, 0] = new Tile(7, 0);

            tiles[0, 1, 0] = new Tile(2, 0);
            tiles[1, 1, 0] = new Tile(5, 0);
            tiles[2, 1, 0] = new Tile(8, 0);

            tiles[0, 2, 0] = new Tile(3, 0);
            tiles[1, 2, 0] = new Tile(6, 0);
            tiles[2, 2, 0] = new Tile(0, 0);
        }

        [Fact]
        public void ShouldHandleNull()
        {
            var initial = Assert.Throws<ArgumentNullException>(()
                => new Printer(null!));

            Assert.Equal(nameof(initial), initial.ParamName);

            var subject = new Printer(new Game(
                FakeTilesDeck.Create(),
                FakeTilesArea.Create(tiles),
                false
            ));

            var game = Assert.Throws<ArgumentNullException>(()
                => subject.Print(null!, 0));

            Assert.Equal(nameof(game), game.ParamName);
        }

        [Fact]
        public void ShouldPrintSingle()
        {
            var expected =
                "            Next: 2            \n" +
                "┌─────────┬─────────┬─────────┐\n" +
                "│         │         │         │\n" +
                "│    3    │    6    │         │\n" +
                "│         │         │         │\n" +
                "├─────────┼─────────┼─────────┤\n" +
                "│         │         │         │\n" +
                "│    2    │    5    │    8    │\n" +
                "│         │         │         │\n" +
                "├─────────┼─────────┼─────────┤\n" +
                "│         │         │         │\n" +
                "│    1    │    4    │    7    │\n" +
                "│         │         │         │\n" +
                "└─────────┴─────────┴─────────┘\n" +
                " Score: 0         Layer: 1 / 1 \n";

            var game = new Game(
                FakeTilesDeck.Create(lottery: new FakeLottery()
                {
                    OnDraw = _ => (new TileHint(new Tile(2, 2)), new Tile(2, 2))
                }).Next(0),
                FakeTilesArea.Create(tiles, merger: new FakeMerger()
                {
                    OnCanMerge = (_, __) => true
                }),
                false
            );

            var subject = new Printer(game);

            var actual = subject.Print(game, 0);

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: IsWindows);
        }

        [Fact]
        public void ShouldPrintEither()
        {
            var expected =
                "          Next: 2 / 4          \n" +
                "┌─────────┬─────────┬─────────┐\n" +
                "│         │         │         │\n" +
                "│    3    │    6    │         │\n" +
                "│         │         │         │\n" +
                "├─────────┼─────────┼─────────┤\n" +
                "│         │         │         │\n" +
                "│    2    │    5    │    8    │\n" +
                "│         │         │         │\n" +
                "├─────────┼─────────┼─────────┤\n" +
                "│         │         │         │\n" +
                "│    1    │    4    │    7    │\n" +
                "│         │         │         │\n" +
                "└─────────┴─────────┴─────────┘\n" +
                " Score: 0         Layer: 1 / 1 \n";

            var game = new Game(
                FakeTilesDeck.Create(lottery: new FakeLottery()
                {
                    OnDraw = _ => (new TileHint(new Tile(2, 2), new Tile(4, 4)), new Tile(2, 2))
                }).Next(0),
                FakeTilesArea.Create(tiles, merger: new FakeMerger()
                {
                    OnCanMerge = (_, __) => true
                }),
                false
            );

            var subject = new Printer(game);

            var actual = subject.Print(game, 0);

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: IsWindows);
        }

        [Fact]
        public void ShouldPrintThrees()
        {
            var expected =
                "        Next: 2 / 4 / 6        \n" +
                "┌─────────┬─────────┬─────────┐\n" +
                "│         │         │         │\n" +
                "│    3    │    6    │         │\n" +
                "│         │         │         │\n" +
                "├─────────┼─────────┼─────────┤\n" +
                "│         │         │         │\n" +
                "│    2    │    5    │    8    │\n" +
                "│         │         │         │\n" +
                "├─────────┼─────────┼─────────┤\n" +
                "│         │         │         │\n" +
                "│    1    │    4    │    7    │\n" +
                "│         │         │         │\n" +
                "└─────────┴─────────┴─────────┘\n" +
                " Score: 0         Layer: 1 / 1 \n";

            var game = new Game(
                FakeTilesDeck.Create(lottery: new FakeLottery()
                {
                    OnDraw = _ => (new TileHint(new Tile(2, 2), new Tile(4, 4), new Tile(6, 6)), new Tile(2, 2))
                }).Next(0),
                FakeTilesArea.Create(tiles, merger: new FakeMerger()
                {
                    OnCanMerge = (_, __) => true
                }),
                false
            );

            var subject = new Printer(game);

            var actual = subject.Print(game, 0);

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: IsWindows);
        }

        [Fact]
        public void ShouldPrintDone()
        {
            var expected =
                "             Done.             \n" +
                "┌─────────┬─────────┬─────────┐\n" +
                "│         │         │         │\n" +
                "│    3    │    6    │         │\n" +
                "│         │         │         │\n" +
                "├─────────┼─────────┼─────────┤\n" +
                "│         │         │         │\n" +
                "│    2    │    5    │    8    │\n" +
                "│         │         │         │\n" +
                "├─────────┼─────────┼─────────┤\n" +
                "│         │         │         │\n" +
                "│    1    │    4    │    7    │\n" +
                "│         │         │         │\n" +
                "└─────────┴─────────┴─────────┘\n" +
                " Score: 0         Layer: 1 / 1 \n";

            var game = new Game(
                FakeTilesDeck.Create(),
                FakeTilesArea.Create(tiles),
                false
            );

            var subject = new Printer(game);

            var actual = subject.Print(game, 0);

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: IsWindows);
        }

        [Fact]
        public void ShouldPrintSmall()
        {
            var tiles = new Tiles(2, 2, 1);

            tiles[0, 0, 0] = new Tile(1, 0);
            tiles[1, 0, 0] = new Tile(3, 0);

            tiles[0, 1, 0] = new Tile(2, 0);
            tiles[1, 1, 0] = new Tile(0, 0);

            var expected =
                "       Next: 2       \n" +
                "┌─────────┬─────────┐\n" +
                "│         │         │\n" +
                "│    2    │         │\n" +
                "│         │         │\n" +
                "├─────────┼─────────┤\n" +
                "│         │         │\n" +
                "│    1    │    3    │\n" +
                "│         │         │\n" +
                "└─────────┴─────────┘\n" +
                " Score: 0            \n";

            var game = new Game(
                FakeTilesDeck.Create(lottery: new FakeLottery()
                {
                    OnDraw = _ => (new TileHint(new Tile(2, 2)), new Tile(2, 2))
                }).Next(0),
                FakeTilesArea.Create(tiles, merger: new FakeMerger()
                {
                    OnCanMerge = (_, __) => true
                }),
                false
            );

            var subject = new Printer(game);

            var actual = subject.Print(game, 0);

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: IsWindows);
        }
    }
}
