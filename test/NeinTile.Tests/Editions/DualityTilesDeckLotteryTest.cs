using NeinTile.Fakes;
using Xunit;
using Xunit.Abstractions;

namespace NeinTile.Editions.Tests
{
    public class DualityTilesDeckLotteryTest
    {
        private readonly ITestOutputHelper output;

        public DualityTilesDeckLotteryTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ShouldDrawRandomSample()
        {
            var area = new TilesArea(
                new FakeTilesAreaMixer(),
                new FakeTilesAreaMerger(),
                new FakeTilesAreaLottery()
            );

            RunTests(area, new TileInfo(0, 100));
        }

        private void RunTests(TilesArea area, TileInfo expected)
        {
            const int testCount = 1000;

            var subject = new DualityTilesDeckLottery();

            var nope = 0;

            for (var i = 0; i < testCount; i++)
            {
                subject = (DualityTilesDeckLottery)subject.CreateNext(area);

                var actual = subject.Draw(out var bonus);

                if (bonus == TileInfo.Empty)
                {
                    Assert.Equal(TileSample.Empty, actual);
                    nope += 1;
                    continue;
                }

                output.WriteLine($"{actual} => {bonus}");

                Assert.True(actual.IsSingle);

                Assert.Equal(expected, actual.First);
                Assert.Equal(expected, actual.Second);
                Assert.Equal(expected, actual.Third);

                Assert.Equal(expected, bonus);
            }

            output.WriteLine($"Sorry: {nope}");

            Assert.NotEqual(0, nope);
            Assert.NotEqual(testCount, nope);
        }
    }
}
