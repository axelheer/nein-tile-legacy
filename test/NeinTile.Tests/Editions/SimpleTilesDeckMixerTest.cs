using Xunit;

namespace NeinTile.Editions.Tests
{
    public class SimpleTilesDeckMixerTest
    {
        [Fact]
        public void ShouldCreateTiles()
        {
            var subject = new SimpleTilesDeckMixer();

            var actual = subject.Shuffle();

            Assert.Collection(actual, item =>
            {
                Assert.Equal(2, item.Value);
                Assert.Equal(2, item.Score);
            });
        }

        [Fact]
        public void ShouldCreateSameTiles()
        {
            var subject = new SimpleTilesDeckMixer();

            var expected = subject.Shuffle();

            var actual = subject.CreateNext().Shuffle();

            Assert.Equal(expected, actual);
        }
    }
}
