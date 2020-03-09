using Xunit;

namespace NeinTile.Tests
{
    public class TileInfoTest
    {
        [Fact]
        public void ShouldBeEmpty()
        {
            var subject = new TileInfo();

            Assert.Equal(TileInfo.Empty, subject);
        }

        [Fact]
        public void ShouldAssignValues()
        {
            var subject = new TileInfo(1, 2);

            Assert.Equal(1, subject.Value);
            Assert.Equal(2, subject.Score);
        }

        [Fact]
        public void ShouldUseValueAsHashCode()
        {
            var subject = new TileInfo(7, 11);

            var actual = subject.GetHashCode();

            Assert.Equal(7, actual);
        }

        [Fact]
        public void ShouldBeEqual()
        {
            var subject = new TileInfo(11, 38);

            var actual = subject.Equals(new TileInfo(11, -1));

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqual()
        {
            var subject = new TileInfo(11, 38);

            var actual = subject.Equals(new TileInfo(-1, 38));

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualAsObject()
        {
            var subject = new TileInfo(11, 38);

            var actual = subject.Equals((object)subject);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualAsObject()
        {
            var subject = new TileInfo(11, 38);

            var actual = subject.Equals(new object());

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingEquals()
        {
            var subject = new TileInfo(11, 38);

            var actual = subject == new TileInfo(11, -1);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingEquals()
        {
            var subject = new TileInfo(11, 38);

            var actual = subject == new TileInfo(-1, 38);

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingNotEquals()
        {
            var subject = new TileInfo(11, 38);

            var actual = subject != new TileInfo(11, -1);

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingNotEquals()
        {
            var subject = new TileInfo(11, 38);

            var actual = subject != new TileInfo(-1, 38);

            Assert.True(actual);
        }
    }
}
