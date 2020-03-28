using System;
using Xunit;

namespace NeinTile.Editions.Tests
{
    public class InsanityTilesDeckMixerTest
    {
        [Fact]
        public void ShouldCreateTiles()
        {
            var subject = new InsanityTilesDeckMixer();

            var actual = subject.Shuffle();

            var (positive, negative) = (0, 0);

            var (ones, twos, threes) = (0, 0, 0);

            Assert.All(actual, item =>
            {
                if (Math.Abs(item.Value) == 1)
                {
                    ones += 1;
                    Assert.Equal(0, item.Score);
                }
                if (Math.Abs(item.Value) == 2)
                {
                    twos += 1;
                    Assert.Equal(0, item.Score);
                }
                if (Math.Abs(item.Value) == 3)
                {
                    threes += 1;
                    Assert.Equal(3, item.Score);
                }
                if (item.Value > 0)
                {
                    positive += 1;
                }
                if (item.Value < 0)
                {
                    negative += 1;
                }
            });

            Assert.Equal(12, actual.Length);
            Assert.Equal(4, ones);
            Assert.Equal(4, twos);
            Assert.Equal(4, threes);
            Assert.Equal(6, positive);
            Assert.Equal(6, negative);
        }

        [Fact]
        public void ShouldCreateSameTiles()
        {
            var subject = new InsanityTilesDeckMixer();

            var expected = subject.Shuffle();

            var actual = subject.Shuffle();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldCreateNotSameTiles()
        {
            var subject = new InsanityTilesDeckMixer();

            var expected = subject.Shuffle();

            var actual = subject.CreateNext().Shuffle();

            Assert.NotEqual(expected, actual);
        }
    }
}
