using System;
using Xunit;

namespace NeinTile.Tests
{
    public class TileIndexTest
    {
        [Fact]
        public void ShouldBeEmpty()
        {
            var subject = new TileIndex();

            Assert.Equal(TileIndex.Empty, subject);
        }

        [Fact]
        public void ShouldAssignValues()
        {
            var (colIndex, rowIndex, layIndex) = new TileIndex(1, 2, 3);

            Assert.Equal(1, colIndex);
            Assert.Equal(2, rowIndex);
            Assert.Equal(3, layIndex);
        }

        [Fact]
        public void ShouldCombineHashCode()
        {
            var subject = new TileIndex(7, 11, 13);

            var expected = HashCode.Combine(subject.Col, subject.Row, subject.Lay);

            var actual = subject.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeEqual()
        {
            var subject = new TileIndex(11, 38, 81);

            var actual = subject.Equals(new TileIndex(11, 38, 81));

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqual()
        {
            var subject = new TileIndex(11, 38, 81);

            var actual = subject.Equals(new TileIndex(-1, 38, 81));

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualAsObject()
        {
            var subject = new TileIndex(11, 38, 81);

            var actual = subject.Equals((object)subject);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualAsObject()
        {
            var subject = new TileIndex(11, 38, 81);

            var actual = subject.Equals(new object());

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingEquals()
        {
            var subject = new TileIndex(11, 38, 81);

            var actual = subject == new TileIndex(11, 38, 81);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingEquals()
        {
            var subject = new TileIndex(11, 38, 81);

            var actual = subject == new TileIndex(11, -1, 81);

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingNotEquals()
        {
            var subject = new TileIndex(11, 38, 81);

            var actual = subject != new TileIndex(11, 38, 81);

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingNotEquals()
        {
            var subject = new TileIndex(11, 38, 81);

            var actual = subject != new TileIndex(11, 38, -1);

            Assert.True(actual);
        }
    }
}
