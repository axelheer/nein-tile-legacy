using NeinTile.Fakes;
using Xunit;
using Xunit.Abstractions;

namespace NeinTile.Editions.Tests
{
    public class InsanityTilesDeckLotteryTest
    {
        private readonly ITestOutputHelper output;

        public InsanityTilesDeckLotteryTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ShouldDrawEmptySample()
        {
            var area = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    Tiles = new TileInfo[2, 2, 1]
                    {
                        { { new TileInfo(0, 200) }, { new TileInfo(0, 200) } },
                        { { new TileInfo(0, 200) }, { new TileInfo(0, 200) } }
                    }
                },
                new FakeTilesAreaMerger(),
                new FakeTilesAreaLottery()
            );

            RunTests(area);
        }

        [Fact]
        public void ShouldDrawNegativeSample()
        {
            var area = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    Tiles = new TileInfo[2, 2, 1]
                    {
                        { { new TileInfo(0, 2_000) }, { new TileInfo(0, 2_000) } },
                        { { new TileInfo(0, 2_000) }, { new TileInfo(0, 2_000) } }
                    }
                },
                new FakeTilesAreaMerger(),
                new FakeTilesAreaLottery()
            );

            RunTests(area,
                new TileInfo(0, -1_000),
                new TileInfo(0, -2_000),
                new TileInfo(0, -4_000),
                new TileInfo(0, -8_000)
            );
        }

        [Fact]
        public void ShouldDrawPositiveSample()
        {
            var area = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    Tiles = new TileInfo[2, 2, 1]
                    {
                        { { new TileInfo(0, 1_000_000) }, { new TileInfo(0, 1_000_000) } },
                        { { new TileInfo(0, 1_000_000) }, { new TileInfo(0, 1_000_000) } }
                    }
                },
                new FakeTilesAreaMerger(),
                new FakeTilesAreaLottery()
            );

            RunTests(area,
                new TileInfo(0, 1_000),
                new TileInfo(0, 2_000),
                new TileInfo(0, 4_000),
                new TileInfo(0, 8_000),
                new TileInfo(0, 16_000),
                new TileInfo(0, 32_000),
                new TileInfo(0, 64_000),
                new TileInfo(0, 128_000),
                new TileInfo(0, 256_000),
                new TileInfo(0, 512_000),
                new TileInfo(0, 1_024_000),
                new TileInfo(0, 2_048_000),
                new TileInfo(0, 4_096_000)
            );
        }

        private void RunTests(TilesArea area, params TileInfo[] expected)
        {
            const int testCount = 1000;

            var subject = new InsanityTilesDeckLottery();

            var nope = 0;

            for (var i = 0; i < testCount; i++)
            {
                subject = (InsanityTilesDeckLottery)subject.CreateNext(area);

                var actual = subject.Draw(out var bonus);

                if (bonus == TileInfo.Empty)
                {
                    Assert.Equal(TileSample.Empty, actual);
                    nope += 1;
                    continue;
                }

                output.WriteLine($"{actual} => {bonus}");

                Assert.Contains(actual.First, expected);
                Assert.Contains(actual.Second, expected);
                Assert.Contains(actual.Third, expected);

                Assert.Contains(bonus, new[] { actual.First, actual.Second, actual.Third });

                if (!actual.IsSingle)
                {
                    Assert.Equal(actual.Second.Value, actual.First.Value * 2);
                    Assert.Equal(actual.Second.Score, actual.First.Score * 3);

                    if (!actual.IsEither)
                    {
                        Assert.Equal(actual.Third.Value, actual.Second.Value * 2);
                        Assert.Equal(actual.Third.Score, actual.Second.Score * 3);
                    }
                }
            }

            output.WriteLine($"Sorry: {nope}");

            if (expected.Length == 0)
            {
                Assert.Equal(testCount, nope);
            }
            else
            {
                Assert.NotEqual(0, nope);
                Assert.NotEqual(testCount, nope);
            }
        }
    }
}
