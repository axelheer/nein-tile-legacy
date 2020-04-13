using System;
using System.Runtime.InteropServices;
using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class GamePrinterTest
    {
        private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        private readonly TileInfo[,,] tiles;

        public GamePrinterTest()
        {
            tiles = new TileInfo[,,]
            {
                {
                    { new TileInfo(1, 0) },
                    { new TileInfo(2, 0) },
                    { new TileInfo(3, 0) },
                },
                {
                    { new TileInfo(4, 0) },
                    { new TileInfo(5, 0) },
                    { new TileInfo(6, 0) },
                },
                {
                    { new TileInfo(7, 0) },
                    { new TileInfo(8, 0) },
                    { new TileInfo(0, 0) },
                }
            };
        }

        [Fact]
        public void ShouldHandleNull()
        {
            var initialState = Assert.Throws<ArgumentNullException>(()
                => new GamePrinter(null!));

            Assert.Equal(nameof(initialState), initialState.ParamName);

            var state = new GameState(
                new FakeTilesDeck(),
                new FakeTilesArea()
            );

            var subject = new GamePrinter(state);

            var gameState = Assert.Throws<ArgumentNullException>(()
                => subject.Print(null!, 0));

            Assert.Equal(nameof(gameState), gameState.ParamName);
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

            var state = new GameState(
                new FakeTilesDeck()
                {
                    OnHint = () => new TileSample(new TileInfo(2, 2))
                },
                new TilesArea(
                    new FakeTilesAreaMixer()
                    {
                        Tiles = tiles
                    },
                    new FakeTilesAreaMerger()
                    {
                        OnCanMerge = (_, __) => true
                    },
                    new FakeTilesAreaLottery()
                )
            );

            var subject = new GamePrinter(state);

            var actual = subject.Print(state, 0);

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

            var state = new GameState(
                new FakeTilesDeck()
                {
                    OnHint = () => new TileSample(new TileInfo(2, 2), new TileInfo(4, 4))
                },
                new TilesArea(
                    new FakeTilesAreaMixer()
                    {
                        Tiles = tiles
                    },
                    new FakeTilesAreaMerger()
                    {
                        OnCanMerge = (_, __) => true
                    },
                    new FakeTilesAreaLottery()
                )
            );

            var subject = new GamePrinter(state);

            var actual = subject.Print(state, 0);

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

            var state = new GameState(
                new FakeTilesDeck()
                {
                    OnHint = () => new TileSample(new TileInfo(2, 2), new TileInfo(4, 4), new TileInfo(6, 6))
                },
                new TilesArea(
                    new FakeTilesAreaMixer()
                    {
                        Tiles = tiles
                    },
                    new FakeTilesAreaMerger()
                    {
                        OnCanMerge = (_, __) => true
                    },
                    new FakeTilesAreaLottery()
                )
            );

            var subject = new GamePrinter(state);

            var actual = subject.Print(state, 0);

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

            var state = new GameState(
                new FakeTilesDeck()
                {
                    OnHint = () => new TileSample(new TileInfo(2, 2))
                },
                new TilesArea(
                    new FakeTilesAreaMixer()
                    {
                        Tiles = tiles
                    },
                    new FakeTilesAreaMerger()
                    {
                        OnCanMerge = (_, __) => false
                    },
                    new FakeTilesAreaLottery()
                )
            );

            var subject = new GamePrinter(state);

            var actual = subject.Print(state, 0);

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: IsWindows);
        }

        [Fact]
        public void ShouldPrintSmall()
        {
            var tiles = new TileInfo[,,]
            {
                {
                    { new TileInfo(1, 0) },
                    { new TileInfo(2, 0) },
                },
                {
                    { new TileInfo(3, 0) },
                    { new TileInfo(0, 0) },
                }
            };

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

            var state = new GameState(
                new FakeTilesDeck()
                {
                    OnHint = () => new TileSample(new TileInfo(2, 2))
                },
                new TilesArea(
                    new FakeTilesAreaMixer()
                    {
                        Tiles = tiles
                    },
                    new FakeTilesAreaMerger()
                    {
                        OnCanMerge = (_, __) => true
                    },
                    new FakeTilesAreaLottery()
                )
            );

            var subject = new GamePrinter(state);

            var actual = subject.Print(state, 0);

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: IsWindows);
        }
    }
}
