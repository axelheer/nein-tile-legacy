using System;
using Xunit;

namespace NeinTile.Tests
{
    public class MoveTest
    {
        [Fact]
        public void ShouldBeEmpty()
        {
            var subject = new Move();

            Assert.Equal(Move.Empty, subject);
        }

        [Fact]
        public void ShouldAssignValues()
        {
            var (source, target, marker) = new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            Assert.Equal(new TileIndex(1, 2, 0), source);
            Assert.Equal(new TileIndex(3, 4, 0), target);
            Assert.Equal(new TileIndex(5, 6, 0), marker);
        }

        [Fact]
        public void ShouldCombineHashCode()
        {
            var subject = new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            var expected = HashCode.Combine(subject.Source, subject.Target, subject.Marker);

            var actual = subject.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeEqual()
        {
            var subject = new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            var actual = subject.Equals(
                new Move(
                    new TileIndex(1, 2, 0),
                    new TileIndex(3, 4, 0),
                    new TileIndex(5, 6, 0)
                )
            );

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqual()
        {
            var subject = new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            var actual = subject.Equals(
                new Move(
                    new TileIndex(-1, 2, 0),
                    new TileIndex(3, 4, 0),
                    new TileIndex(5, 6, 0)
                )
            );

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualAsObject()
        {
            var subject = new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            var actual = subject.Equals((object)subject);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualAsObject()
        {
            var subject = new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            var actual = subject.Equals(new object());

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingEquals()
        {
            var subject = new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            var actual = subject == new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingEquals()
        {
            var subject = new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            var actual = subject == new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(-3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingNotEquals()
        {
            var subject = new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            var actual = subject != new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingNotEquals()
        {
            var subject = new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(5, 6, 0)
            );

            var actual = subject != new Move(
                new TileIndex(1, 2, 0),
                new TileIndex(3, 4, 0),
                new TileIndex(-5, 6, 0)
            );

            Assert.True(actual);
        }
    }
}
