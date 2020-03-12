using System;
using Xunit;

namespace NeinTile.Tests
{
    public class TileMoveTest
    {
        [Fact]
        public void ShouldBeEmpty()
        {
            var subject = new TileMove();

            Assert.Equal(TileMove.Empty, subject);
        }

        [Fact]
        public void ShouldAssignValues()
        {
            var (source, target) = new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, 4)
            );

            Assert.Equal(new TileInfo(1, 2), source);
            Assert.Equal(new TileInfo(3, 4), target);
        }

        [Fact]
        public void ShouldCombineHashCode()
        {
            var subject = new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, 4)
            );

            var expected = HashCode.Combine(subject.Source, subject.Target);

            var actual = subject.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeEqual()
        {
            var subject = new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, 4)
            );

            var actual = subject.Equals(
                new TileMove(
                    new TileInfo(1, 2),
                    new TileInfo(3, 4)
                )
            );

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqual()
        {
            var subject = new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, 4)
            );

            var actual = subject.Equals(
                new TileMove(
                    new TileInfo(1, 2),
                    new TileInfo(3, -1)
                )
            );

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualAsObject()
        {
            var subject = new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, 4)
            );

            var actual = subject.Equals((object)subject);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualAsObject()
        {
            var subject = new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, -1)
            );

            var actual = subject.Equals(new object());

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingEquals()
        {
            var subject = new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, 4)
            );

            var actual = subject == new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, 4)
            );

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingEquals()
        {
            var subject = new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, 4)
            );

            var actual = subject == new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, -1)
            );

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingNotEquals()
        {
            var subject = new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, 4)
            );

            var actual = subject != new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, 4)
            );

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingNotEquals()
        {
            var subject = new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, 4)
            );

            var actual = subject != new TileMove(
                new TileInfo(1, 2),
                new TileInfo(3, -1)
            );

            Assert.True(actual);
        }
    }
}
