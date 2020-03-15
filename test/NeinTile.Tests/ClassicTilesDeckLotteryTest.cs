using System;
using NeinTile.Fakes;
using Xunit;
using Xunit.Abstractions;

namespace NeinTile.Tests
{
    public class ClassicTilesDeckLotteryTest
    {
        private readonly ITestOutputHelper output;

        public ClassicTilesDeckLotteryTest(ITestOutputHelper output)
            => this.output = output;

        [Fact]
        public void ShouldDrawEmptySample()
        {
            var area = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    OnShuffle = () => new TileInfo[2, 2, 1]
                    {
                        { { new TileInfo(1, 0) }, { new TileInfo(3, 0) } },
                        { { new TileInfo(2, 0) }, { new TileInfo(4, 0) } }
                    }
                },
                new FakeTilesAreaMerger(),
                new FakeTilesAreaLottery()
            );

            RunTests(area);
        }

        [Fact]
        public void ShouldDrawSingleSample()
        {
            var area = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    OnShuffle = () => new TileInfo[2, 2, 1]
                    {
                        { { new TileInfo(48, 0) }, { new TileInfo(12, 0) } },
                        { { new TileInfo(24, 0) }, { new TileInfo(24, 0) } }
                    }
                },
                new FakeTilesAreaMerger(),
                new FakeTilesAreaLottery()
            );

            RunTests(area, new TileInfo(6, 9));
        }

        [Fact]
        public void ShouldDrawEitherSample()
        {
            var area = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    OnShuffle = () => new TileInfo[2, 2, 1]
                    {
                        { { new TileInfo(48, 0) }, { new TileInfo(12, 0) } },
                        { { new TileInfo(24, 0) }, { new TileInfo(96, 0) } }
                    }
                },
                new FakeTilesAreaMerger(),
                new FakeTilesAreaLottery()
            );

            RunTests(area, new TileInfo(6, 9), new TileInfo(12, 27));
        }

        [Fact]
        public void ShouldDrawRandomSample()
        {
            var area = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    OnShuffle = () => new TileInfo[2, 2, 1]
                    {
                        { { new TileInfo(384, 0) }, { new TileInfo(192, 0) } },
                        { { new TileInfo(768, 0) }, { new TileInfo(192, 0) } }
                    }
                },
                new FakeTilesAreaMerger(),
                new FakeTilesAreaLottery()
            );

            RunTests(area,
                new TileInfo(6, 9),
                new TileInfo(12, 27),
                new TileInfo(24, 81),
                new TileInfo(48, 243),
                new TileInfo(96, 729)
            );
        }

        private void RunTests(TilesArea area, params TileInfo[] expected)
        {
            const int testCount = 1000;

            ITilesDeckLottery subject = new ClassicTilesDeckLottery();

            var nope = 0;

            for (var i = 0; i < testCount; i++)
            {
                subject = subject.CreateNext(area);

                var actual = subject.Draw(out var bonus);

                if (bonus == TileInfo.Empty)
                {
                    Assert.Equal(TileSample.Empty, actual);
                    nope = nope + 1;
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
