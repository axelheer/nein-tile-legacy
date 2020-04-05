using System;
using Xunit;

namespace NeinTile.Editions.Tests
{
    public class DualityTilesDeckMixerTest
    {
        [Fact]
        public void ShouldCreateTiles()
        {
            var subject = new DualityTilesDeckMixer();

            var actual = subject.Shuffle();

            var (positive, negative) = (0, 0);

            Assert.All(actual, item =>
            {
                if (item.Value > 0)
                {
                    positive += 1;
                }
                if (item.Value < 0)
                {
                    negative += 1;
                }
                Assert.Equal(2, Math.Abs(item.Value));
            });

            Assert.Equal(4, actual.Length);
            Assert.Equal(2, positive);
            Assert.Equal(2, negative);
        }

        [Fact]
        public void ShouldCreateSameTiles()
        {
            var subject = new DualityTilesDeckMixer();

            var expected = subject.Shuffle();

            var actual = subject.Shuffle();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldCreateNotSameTiles()
        {
            var subject = new DualityTilesDeckMixer();

            var expected = subject.Shuffle();

            var actual = subject.CreateNext().Shuffle();

            Assert.NotEqual(expected, actual);
        }
    }
}
