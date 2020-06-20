using System;
using Xunit;

namespace NeinTile.Editions.Tests
{
    public class InsanityMixerTest
    {
        [Fact]
        public void ShouldCreateTiles()
        {
            var subject = new InsanityMixer();

            var actual = subject.Mix();

            var (positive, negative) = (0, 0);

            var (ones, twos, threes) = (0, 0, 0);

            Assert.All(actual, item =>
            {
                if (Math.Abs(item.Value) == 1)
                {
                    ones += 1;
                    Assert.Equal(1, item.Score);
                }
                if (Math.Abs(item.Value) == 2)
                {
                    twos += 1;
                    Assert.Equal(2, item.Score);
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

            Assert.Equal(18, actual.Length);
            Assert.Equal(6, ones);
            Assert.Equal(6, twos);
            Assert.Equal(6, threes);
            Assert.Equal(12, positive);
            Assert.Equal(6, negative);
        }
    }
}
