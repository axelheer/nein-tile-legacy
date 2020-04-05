using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class GamePrinterTest
    {
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
                    { new TileInfo(9, 0) },
                }
            };
        }

        [Fact]
        public void ShouldPrintSingle()
        {
            var expected =
                "            Next: 2            \n" +
                "┌─────────┬─────────┬─────────┐\n" +
                "│         │         │         │\n" +
                "│    3    │    6    │    9    │\n" +
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
                        OnShuffle = () => tiles
                    },
                    new FakeTilesAreaMerger()
                    {
                        OnCanMerge = (_, __) => true
                    },
                    new FakeTilesAreaLottery()
                )
            );

            var subject = new GamePrinter(3, 3);

            var actual = subject.Print(state, 0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldPrintEither()
        {
            var expected =
                "          Next: 2 / 4          \n" +
                "┌─────────┬─────────┬─────────┐\n" +
                "│         │         │         │\n" +
                "│    3    │    6    │    9    │\n" +
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
                        OnShuffle = () => tiles
                    },
                    new FakeTilesAreaMerger()
                    {
                        OnCanMerge = (_, __) => true
                    },
                    new FakeTilesAreaLottery()
                )
            );

            var subject = new GamePrinter(3, 3);

            var actual = subject.Print(state, 0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldPrintThrees()
        {
            var expected =
                "        Next: 2 / 4 / 6        \n" +
                "┌─────────┬─────────┬─────────┐\n" +
                "│         │         │         │\n" +
                "│    3    │    6    │    9    │\n" +
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
                        OnShuffle = () => tiles
                    },
                    new FakeTilesAreaMerger()
                    {
                        OnCanMerge = (_, __) => true
                    },
                    new FakeTilesAreaLottery()
                )
            );

            var subject = new GamePrinter(3, 3);

            var actual = subject.Print(state, 0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldPrintDone()
        {
            var expected =
                "             Done.             \n" +
                "┌─────────┬─────────┬─────────┐\n" +
                "│         │         │         │\n" +
                "│    3    │    6    │    9    │\n" +
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
                        OnShuffle = () => tiles
                    },
                    new FakeTilesAreaMerger()
                    {
                        OnCanMerge = (_, __) => false
                    },
                    new FakeTilesAreaLottery()
                )
            );

            var subject = new GamePrinter(3, 3);

            var actual = subject.Print(state, 0);

            Assert.Equal(expected, actual);
        }
    }
}
