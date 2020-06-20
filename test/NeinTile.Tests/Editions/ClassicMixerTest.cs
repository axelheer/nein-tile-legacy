using Xunit;

namespace NeinTile.Editions.Tests
{
    public class ClassicMixerTest
    {
        [Fact]
        public void ShouldCreateTiles()
        {
            var subject = new ClassicMixer();

            var actual = subject.Mix();

            var (ones, twos, threes) = (0, 0, 0);

            Assert.All(actual, item =>
            {
                if (item.Value == 1)
                {
                    ones += 1;
                    Assert.Equal(0, item.Score);
                }
                if (item.Value == 2)
                {
                    twos += 1;
                    Assert.Equal(0, item.Score);
                }
                if (item.Value == 3)
                {
                    threes += 1;
                    Assert.Equal(3, item.Score);
                }
            });

            Assert.Equal(12, actual.Length);
            Assert.Equal(4, ones);
            Assert.Equal(4, twos);
            Assert.Equal(4, threes);
        }
    }
}
