using Xunit;

namespace NeinTile.Editions.Tests
{
    public class SimpleMixerTest
    {
        [Fact]
        public void ShouldCreateTiles()
        {
            var subject = new SimpleMixer();

            var actual = subject.Mix();

            Assert.Collection(actual, item =>
            {
                Assert.Equal(2, item.Value);
                Assert.Equal(2, item.Score);
            });
        }
    }
}
