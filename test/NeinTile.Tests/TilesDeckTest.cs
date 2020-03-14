using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class TilesDeckTest
    {
        [Fact]
        public void ShouldPreviewBonus()
        {
            var expected = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(2, 3),
                new TileInfo(3, 4)
            );

            var subject = new TilesDeck(
                new FakeTilesDeckMixer(),
                new FakeTilesDeckLottery()
                {
                    OnDraw = _ => (expected, expected.Second)
                }
            );

            var actual = subject.Preview();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldPreviewNextTile()
        {
            var expected = new TileSample(
                new TileInfo(1, 2)
            );

            var subject = new TilesDeck(
                new FakeTilesDeckMixer()
                {
                    OnShuffle = () => new[] { expected.First }
                },
                new FakeTilesDeckLottery()
            );

            var actual = subject.Preview();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldDrawBonus()
        {
            var expected = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(2, 3),
                new TileInfo(3, 4)
            );

            var subject = new TilesDeck(
                new FakeTilesDeckMixer()
                {
                    OnShuffle = () => new[] { expected.First, expected.Third }
                },
                new FakeTilesDeckLottery()
                {
                    OnDraw = _ => (expected, expected.Second)
                }
            );

            var actual = subject.Draw(out var actualNextTile);

            Assert.Equal(expected.Second, actualNextTile);
            Assert.Equal(2, actual.Size);
            Assert.Equal(expected.First, actual[0]);
            Assert.Equal(expected.Third, actual[1]);
        }

        [Fact]
        public void ShouldDrawNextTile()
        {
            var expected = new TileInfo(1, 2);
            var unexpected = new TileInfo(2, 3);

            var subject = new TilesDeck(
                new FakeTilesDeckMixer()
                {
                    OnShuffle = () => new[] { expected, unexpected }
                },
                new FakeTilesDeckLottery()
            );

            var actual = subject.Draw(out var actualNextTile);

            Assert.Equal(expected, actualNextTile);
            Assert.Equal(1, actual.Size);
            Assert.Equal(unexpected, actual[0]);
        }

        [Fact]
        public void ShouldDrawNewMixer()
        {
            var expected = new TileInfo(1, 2);

            var subject = new TilesDeck(
                new FakeTilesDeckMixer()
                {
                    OnShuffle = () => new TileInfo[1],
                    OnCreateNext = () => new FakeTilesDeckMixer()
                    {
                        OnShuffle = () => new[] { expected }
                    }
                },
                new FakeTilesDeckLottery()
            );

            var actual = subject.Draw(out _);

            Assert.Equal(1, actual.Size);
            Assert.Equal(expected, actual[0]);
        }

        [Fact]
        public void ShouldDrawNewLottery()
        {
            var expected = new TileSample(new TileInfo(1, 2));

            var subject = new TilesDeck(
                new FakeTilesDeckMixer()
                {
                    OnShuffle = () => new TileInfo[1]
                },
                new FakeTilesDeckLottery()
                {
                    OnCreateNext = () => new FakeTilesDeckLottery()
                    {
                        OnDraw = _ => (expected, expected.First)
                    }
                }
            );

            var actual = subject.Draw(out _).Preview();

            Assert.Equal(expected, actual);
        }
    }
}
