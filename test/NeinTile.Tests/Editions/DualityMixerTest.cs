using System;
using Xunit;

namespace NeinTile.Editions.Tests
{
    public class DualityMixerTest
    {
        [Fact]
        public void ShouldCreateTiles()
        {
            var subject = new DualityMixer();

            var actual = subject.Mix();

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

            Assert.Equal(3, actual.Length);
            Assert.Equal(2, positive);
            Assert.Equal(1, negative);
        }
    }
}
