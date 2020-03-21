using Xunit;

namespace NeinTile.Rules.Tests
{
    public class ClassicTilesDeckMixerTest
    {
        [Fact]
        public void ShouldCreateTiles()
        {
            var subject = new ClassicTilesDeckMixer();

            var actual = subject.Shuffle();

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

        [Fact]
        public void ShouldCreateSameTiles()
        {
            var subject = new ClassicTilesDeckMixer();

            var expected = subject.Shuffle();

            var actual = subject.Shuffle();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldCreateNotSameTiles()
        {
            var subject = new ClassicTilesDeckMixer();

            var expected = subject.Shuffle();

            var actual = subject.CreateNext().Shuffle();

            Assert.NotEqual(expected, actual);
        }
    }
}
