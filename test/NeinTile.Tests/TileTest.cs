using System;
using Xunit;

namespace NeinTile.Tests
{
    public class TileTest
    {
        [Fact]
        public void ShouldBeEmpty()
        {
            var subject = new Tile();

            Assert.Equal(Tile.Empty, subject);
        }

        [Fact]
        public void ShouldAssignValues()
        {
            var (value, score) = new Tile(1, 2);

            Assert.Equal(1, value);
            Assert.Equal(2, score);
        }

        [Fact]
        public void ShouldCombineHashCode()
        {
            var subject = new Tile(7, 11);

            var expected = HashCode.Combine(subject.Value, subject.Score);

            var actual = subject.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeEqual()
        {
            var subject = new Tile(11, 38);

            var actual = subject.Equals(new Tile(11, 38));

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqual()
        {
            var subject = new Tile(11, 38);

            var actual = subject.Equals(new Tile(-11, 38));

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualAsObject()
        {
            var subject = new Tile(11, 38);

            var actual = subject.Equals((object)subject);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualAsObject()
        {
            var subject = new Tile(11, 38);

            var actual = subject.Equals(new object());

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingEquals()
        {
            var subject = new Tile(11, 38);

            var actual = subject == new Tile(11, 38);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingEquals()
        {
            var subject = new Tile(11, 38);

            var actual = subject == new Tile(11, -38);

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingNotEquals()
        {
            var subject = new Tile(11, 38);

            var actual = subject != new Tile(11, 38);

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingNotEquals()
        {
            var subject = new Tile(11, 38);

            var actual = subject != new Tile(-11, 38);

            Assert.True(actual);
        }
    }
}
