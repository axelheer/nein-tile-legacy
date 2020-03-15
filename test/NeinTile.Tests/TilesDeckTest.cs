using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class TilesDeckTest
    {
        [Fact]
        public void ShouldHintBonus()
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
                    OnDraw = () => (expected, expected.Second)
                }
            );

            var actual = subject.Hint();

            Assert.True(subject.IsBonus);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldHintNextTile()
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

            var actual = subject.Hint();

            Assert.False(subject.IsBonus);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldShowBonus()
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
                    OnDraw = () => (expected, expected.Second)
                }
            );

            var actual = subject.Show();

            Assert.True(subject.IsBonus);
            Assert.Equal(expected.Second, actual);
        }

        [Fact]
        public void ShouldShowNextTile()
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

            var actual = subject.Show();

            Assert.False(subject.IsBonus);
            Assert.Equal(expected, actual);
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

            var actual = subject.Draw(null);

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
                    OnCreateNext = _ => new FakeTilesDeckLottery()
                    {
                        OnDraw = () => (expected, expected.First)
                    }
                }
            );

            var actual = subject.Draw(null).Hint();

            Assert.Equal(expected, actual);
        }
    }
}
